// Controllers/AuthController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using SnapSaves.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Security.Claims;
using SnapSaves.Helpers;

namespace SnapSaves.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly MongoDbContext _mongoDb;
        private readonly ILogger<AuthController> _logger;
        private readonly AppIdentityDbContext _context;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            MongoDbContext mongoDb,
            ILogger<AuthController> logger,
            AppIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mongoDb = mongoDb;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            // Use the same GoogleLogin flow for registration
            return GoogleLogin(returnUrl);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult GoogleLogin(string? returnUrl = null)
        {
            var redirectUrl = Url.Action("HandleGoogleResponse", "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet]
        public async Task<IActionResult> HandleGoogleResponse(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    _logger.LogWarning("External login info was null");
                    TempData["Error"] = "Could not retrieve your information from Google. Please try again.";
                    return RedirectToAction("Login");
                }

                _logger.LogInformation("Google login attempt for provider: {Provider}, Key: {Key}", 
                    info.LoginProvider, info.ProviderKey);

                // Try to sign in the user with this external login provider
                var result = await _signInManager.ExternalLoginSignInAsync(
                    info.LoginProvider, 
                    info.ProviderKey, 
                    isPersistent: false, 
                    bypassTwoFactor: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in with {Name} provider", info.LoginProvider);
                    return LocalRedirect(returnUrl);
                }

                // If the user does not have an account, create one
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Email claim not found from Google");
                    TempData["Error"] = "Unable to get email from Google account";
                    return RedirectToAction("Login");
                }

                var firstName = info.Principal.FindFirstValue("given_name") ?? 
                               info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "";
                var lastName = info.Principal.FindFirstValue("family_name") ?? 
                              info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "";

                // Check if user already exists in the database
                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser != null)
                {
                    // User exists but doesn't have this external login
                    var addLoginResult = await _userManager.AddLoginAsync(existingUser, info);
                    if (addLoginResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(existingUser, isPersistent: false);
                        _logger.LogInformation("Added external login for existing user {Email}", email);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        _logger.LogError("Failed to add external login for existing user {Email}: {Errors}", 
                            email, string.Join(", ", addLoginResult.Errors.Select(e => e.Description)));
                        TempData["Error"] = "Failed to link Google account to existing user.";
                        return RedirectToAction("Login");
                    }
                }

                // Create new user with organization and course
                var newUser = await CreateUserFromGoogleAsync(email, firstName, lastName, info);
                
                if (newUser != null)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    _logger.LogInformation("Successfully created and signed in new user {Email}", email);
                    return LocalRedirect(returnUrl);
                }

                TempData["Error"] = "Failed to create user account";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Google authentication");
                TempData["Error"] = $"Authentication error: {ex.Message}";
                return RedirectToAction("Login");
            }
        }

        private async Task<AppUser?> CreateUserFromGoogleAsync(string email, string firstName, string lastName, ExternalLoginInfo info)
        {
            // Use MySQL execution strategy to handle transactions with retry logic
            var strategy = _context.Database.CreateExecutionStrategy();
            
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    // Step 1: Create the user in MongoDB
                    var mongoUser = new User
                    {
                        Username = email,
                        Email = email,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _mongoDb.Users.InsertOneAsync(mongoUser);

                    // Step 2: Create a single-user organization
                    var organizationName = $"SingleUser-{email}";
                    var organization = new Organization
                    {
                        Name = organizationName,
                        Description = $"Personal organization for {firstName} {lastName}",
                        ToolConsumerInstanceGuid = Guid.NewGuid().ToString() // Generate a unique GUID for single users
                    };

                    _context.Organizations.Add(organization);
                    await _context.SaveChangesAsync();

                    // Step 3: Create the user in MySQL
                    var appUser = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        MongoUserId = mongoUser.Id,
                        CreatedAt = DateTime.UtcNow,
                        OrganizationId = organization.Id,
                        EmailConfirmed = true // Google emails are pre-verified
                    };

                    var result = await _userManager.CreateAsync(appUser);

                    if (!result.Succeeded)
                    {
                        // Rollback MongoDB user
                        await _mongoDb.Users.DeleteOneAsync(u => u.Id == mongoUser.Id);
                        _logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                        throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }

                    // Step 4: Create a default course for the organization
                    var defaultCourse = new Course
                    {
                        Name = "My Projects",
                        Description = $"Default course for {firstName} {lastName}'s projects",
                        OrganizationId = organization.Id
                    };

                    _context.Courses.Add(defaultCourse);
                    await _context.SaveChangesAsync();

                    // Step 5: Enroll the user in the default course
                    _context.UserCourses.Add(new UserCourse
                    {
                        UserId = appUser.Id,
                        CourseId = defaultCourse.Id
                    });

                    // Step 6: Add external login
                    var addLoginResult = await _userManager.AddLoginAsync(appUser, info);
                    if (!addLoginResult.Succeeded)
                    {
                        _logger.LogError("Failed to add external login: {Errors}", string.Join(", ", addLoginResult.Errors.Select(e => e.Description)));
                        // Don't fail the whole process for this - user can still log in
                    }

                    // Step 7: Assign the user to the "Student" role (default for single users)
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Student");
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Failed to assign role: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                        // Don't fail the whole process for this
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully created user {Email} with organization {OrganizationName} and default course {CourseName}", 
                        email, organizationName, defaultCourse.Name);
                    
                    return appUser;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error creating user from Google OAuth for {Email}", email);
                    
                    // Cleanup MongoDB user if it was created
                    try
                    {
                        await _mongoDb.Users.DeleteOneAsync(u => u.Username == email);
                    }
                    catch (Exception cleanupEx)
                    {
                        _logger.LogError(cleanupEx, "Failed to cleanup MongoDB user during rollback");
                    }
                    
                    throw; // Re-throw to let ExecutionStrategy handle retries if needed
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return RedirectToAction("Index", "Home");
        }
    }
}
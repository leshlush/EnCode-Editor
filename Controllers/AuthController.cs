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

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            MongoDbContext mongoDb,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mongoDb = mongoDb;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Step 1: Create the user in MongoDB
                var mongoUser = new User
                {
                    Username = model.Email,
                    Email = model.Email,
                    CreatedAt = DateTime.UtcNow
                };

                await _mongoDb.Users.InsertOneAsync(mongoUser);

                // Step 2: Retrieve the default organization from MySQL
                var defaultOrganization = await _userManager.Users
                    .Select(u => u.Organization)
                    .FirstOrDefaultAsync(o => o.Name == "Default Organization");

                if (defaultOrganization == null)
                {
                    ModelState.AddModelError(string.Empty, "Default organization not found.");
                    return View(model);
                }

                // Step 3: Create the user in MySQL with the MongoUserId and OrganizationId
                var appUser = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MongoUserId = mongoUser.Id, // Use the MongoDB ID
                    CreatedAt = DateTime.UtcNow,
                    OrganizationId = defaultOrganization.Id // Use the OrganizationId
                };

                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (!result.Succeeded)
                {
                    // Rollback MongoDB user if MySQL user creation fails
                    await _mongoDb.Users.DeleteOneAsync(u => u.Id == mongoUser.Id);
                    ModelState.AddModelError(string.Empty, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return View(model);
                }

                // Step 4: Assign the user to the "Student" role
                var roleResult = await _userManager.AddToRoleAsync(appUser, "Student");
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                ModelState.AddModelError(string.Empty, "An error occurred during registration.");
                return View(model);
            }
        }



        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Add MongoUserId claim to the user's claims
                    var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var identity = (ClaimsIdentity)userPrincipal.Identity;

                    if (!identity.HasClaim(c => c.Type == "MongoUserId"))
                    {
                        identity.AddClaim(new Claim("MongoUserId", user.MongoUserId));
                    }

                    await HttpContext.SignInAsync(userPrincipal);

                    _logger.LogInformation("User logged in");
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                ModelState.AddModelError("", "An error occurred during login");
                return View(model);
            }
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
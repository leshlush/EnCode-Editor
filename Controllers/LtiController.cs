using LtiLibrary.NetCore.Lti.v1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SnapSaves.Controllers
{
    public class LtiController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly MongoDbContext _mongoDb;

        public LtiController(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            MongoDbContext mongoDb)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mongoDb = mongoDb;
        }

        [HttpPost]
        public async Task<IActionResult> Launch()
        {
            // Parse the LTI request
            var ltiRequest = await ParseLtiRequestAsync();

            // Determine the user's role
            string userRole = DetermineUserRole(ltiRequest);

            // Check if the user exists or create a new one
            var identityUser = await GetOrCreateUserAsync(ltiRequest, userRole);

            // Log the user in
            await _signInManager.SignInAsync(identityUser, isPersistent: false);

            // Return the LTI launch view
            return View("LtiLaunch", ltiRequest);
        }

        private async Task<LtiRequest> ParseLtiRequestAsync()
        {
            var form = await HttpContext.Request.ReadFormAsync();

            var ltiRequest = new LtiRequest
            {
                ConsumerKey = form["oauth_consumer_key"],
                HttpMethod = HttpContext.Request.Method,
                Url = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}")
            };

            foreach (var kvp in form)
            {
                ltiRequest.AddParameter(kvp.Key, kvp.Value);
            }

            return ltiRequest;
        }

        private string DetermineUserRole(LtiRequest ltiRequest)
        {
            if (!string.IsNullOrEmpty(ltiRequest.Roles))
            {
                if (ltiRequest.Roles.Contains("Instructor", StringComparison.OrdinalIgnoreCase))
                {
                    return "Teacher";
                }
                else if (ltiRequest.Roles.Contains("Learner", StringComparison.OrdinalIgnoreCase))
                {
                    return "Student";
                }
            }

            return "Student"; // Default to "Student"
        }

        private async Task<AppUser> GetOrCreateUserAsync(LtiRequest ltiRequest, string userRole)
        {
            var identityUser = await _userManager.FindByNameAsync(ltiRequest.UserId);

            if (identityUser == null)
            {
                identityUser = await CreateUserAsync(ltiRequest, userRole);
            }
            else
            {
                await EnsureUserHasRoleAsync(identityUser, userRole);
            }

            return identityUser;
        }

        private async Task<AppUser> CreateUserAsync(LtiRequest ltiRequest, string userRole)
        {
            // Get or create the organization
            var organization = await GetOrCreateOrganization(ltiRequest);

            // Create the new user and assign them to the organization
            var newUser = new AppUser
            {
                UserName = ltiRequest.UserId,
                Email = $"{ltiRequest.UserId}@example.com",
                FirstName = "DefaultFirstName",
                LastName = "DefaultLastName",
                MongoUserId = ObjectId.GenerateNewId().ToString(),
                CreatedAt = DateTime.UtcNow,
                OrganizationId = organization.Id // Assign the user to the organization
            };

            var result = await _userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create user.");
            }

            await _userManager.AddToRoleAsync(newUser, userRole);
            await AssignUserToDefaultCourseAsync(newUser);

            return newUser;
        }

        private async Task<Organization> GetOrCreateOrganization(LtiRequest ltiRequest)
        {
            string organizationName = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "organization_name").Value ?? "Default Organization";

            string organizationDescription = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "organization_description").Value ?? "Default Description";

            // Try to get the organization
            var organization = GetOrganization(organizationName);

            // If not found, create a new one
            if (organization == null)
            {
                organization = await CreateOrganization(organizationName, organizationDescription);
            }

            return organization;
        }

        private Organization GetOrganization(string organizationName)
        {
            return _context.Organizations.FirstOrDefault(o => o.Name == organizationName);
        }

        private async Task<Organization> CreateOrganization(string name, string description)
        {
            var organization = new Organization
            {
                Name = name,
                Description = description,
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return organization;
        }



        private async Task EnsureUserHasRoleAsync(AppUser user, string userRole)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any())
            {
                var result = await _userManager.AddToRoleAsync(user, userRole);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Failed to assign role.");
                }
            }
        }

        private async Task AssignUserToDefaultCourseAsync(AppUser user)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Name == "Math 101");

            if (course != null)
            {
                _context.UserCourses.Add(new UserCourse
                {
                    UserId = user.Id,
                    CourseId = course.Id
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}

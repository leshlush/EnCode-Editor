using LtiLibrary.NetCore.Lti.v1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore;

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

            // Get or create the organization and its default course
            var organization = await GetOrCreateOrganization(ltiRequest);
            var defaultCourse = await GetOrCreateDefaultCourseAsync(organization);

            // Check if the user exists or create a new one
            var identityUser = await GetOrCreateUserAsync(ltiRequest, userRole, organization);

            // Enroll the user in the default course if not already enrolled
            await EnrollUserInDefaultCourseAsync(identityUser, defaultCourse);

            // Log the user in
            await _signInManager.SignInAsync(identityUser, isPersistent: false);

            // Redirect to the Course Details view of the organization's default course
            return RedirectToAction("Details", "Courses", new { id = defaultCourse.Id });
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

        private async Task<AppUser> GetOrCreateUserAsync(LtiRequest ltiRequest, string userRole, Organization organization)
        {
            string toolConsumerInstanceGuid = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "tool_consumer_instance_guid").Value;
            string userId = ltiRequest.UserId;
            string uniqueUserId = $"{toolConsumerInstanceGuid}:{userId}";

            var identityUser = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == uniqueUserId && u.OrganizationId == organization.Id);

            if (identityUser == null)
            {
                identityUser = await CreateUserAsync(ltiRequest, userRole, uniqueUserId, organization);
            }
            else
            {
                await EnsureUserHasRoleAsync(identityUser, userRole);
            }

            return identityUser;
        }

        private async Task<AppUser> CreateUserAsync(LtiRequest ltiRequest, string userRole, string username, Organization organization)
        {
            var newUser = new AppUser
            {
                UserName = username,
                Email = $"{ltiRequest.UserId}@example.com",
                FirstName = "DefaultFirstName",
                LastName = "DefaultLastName",
                MongoUserId = ObjectId.GenerateNewId().ToString(),
                CreatedAt = DateTime.UtcNow,
                OrganizationId = organization.Id
            };

            var result = await _userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create user.");
            }

            await _userManager.AddToRoleAsync(newUser, userRole);

            return newUser;
        }

        private async Task<Organization> GetOrCreateOrganization(LtiRequest ltiRequest)
        {
            string toolConsumerInstanceGuid = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "tool_consumer_instance_guid").Value;
            string toolConsumerInstanceName = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "tool_consumer_instance_name").Value;
            string organizationName = $"{toolConsumerInstanceName} ({toolConsumerInstanceGuid})";
            string organizationDescription = ltiRequest.Parameters.FirstOrDefault(p => p.Key == "organization_description").Value ?? "Default Description";

            var organization = _context.Organizations
                .FirstOrDefault(o => o.Name == organizationName && o.ToolConsumerInstanceGuid == toolConsumerInstanceGuid);

            if (organization == null)
            {
                organization = await CreateOrganization(organizationName, organizationDescription, toolConsumerInstanceGuid);
            }

            return organization;
        }

        private async Task<Organization> CreateOrganization(string name, string description, string toolConsumerInstanceGuid)
        {
            var organization = new Organization
            {
                Name = name,
                Description = description,
                ToolConsumerInstanceGuid = toolConsumerInstanceGuid,
                Type = OrganizationType.LtiIntegration, // Set the type for LTI organizations
                CreatedAt = DateTime.UtcNow
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return organization;
        }

        private async Task<Course> GetOrCreateDefaultCourseAsync(Organization organization)
        {
            // Use a consistent default course name
            string defaultCourseName = "Default Course";

            var defaultCourse = await _context.Courses
                .FirstOrDefaultAsync(c => c.OrganizationId == organization.Id && c.Name == defaultCourseName);

            if (defaultCourse == null)
            {
                defaultCourse = new Course
                {
                    Name = defaultCourseName,
                    Description = $"Default course for {organization.Name}",
                    OrganizationId = organization.Id
                };
                _context.Courses.Add(defaultCourse);
                await _context.SaveChangesAsync();
            }

            return defaultCourse;
        }

        private async Task EnrollUserInDefaultCourseAsync(AppUser user, Course defaultCourse)
        {
            var alreadyEnrolled = await _context.UserCourses
                .AnyAsync(uc => uc.UserId == user.Id && uc.CourseId == defaultCourse.Id);

            if (!alreadyEnrolled)
            {
                _context.UserCourses.Add(new UserCourse
                {
                    UserId = user.Id,
                    CourseId = defaultCourse.Id
                });
                await _context.SaveChangesAsync();
            }
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
    }
}

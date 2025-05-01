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
        private readonly MongoDbContext _mongoDb; // Add MongoDbContext

        public LtiController(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            MongoDbContext mongoDb) // Inject MongoDbContext
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mongoDb = mongoDb; // Assign MongoDbContext
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Launch()
        {
            // Read the form data from the HTTP request
            var form = await HttpContext.Request.ReadFormAsync();

            // Create and initialize the LtiRequest object
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

            // Determine the user's role based on the LTI request
            string userRole = "Student"; // Default to "Student"
            if (!string.IsNullOrEmpty(ltiRequest.Roles))
            {
                if (ltiRequest.Roles.Contains("Instructor", StringComparison.OrdinalIgnoreCase))
                {
                    userRole = "Teacher";
                }
                else if (ltiRequest.Roles.Contains("Learner", StringComparison.OrdinalIgnoreCase))
                {
                    userRole = "Student";
                }
            }

            // Check if the user exists in the AspNetUsers table
            var identityUser = await _userManager.FindByNameAsync(ltiRequest.UserId);

            if (identityUser == null)
            {
                // Step 1: Create a new user
                identityUser = new AppUser
                {
                    UserName = ltiRequest.UserId,
                    Email = $"{ltiRequest.UserId}@example.com",
                    FirstName = "DefaultFirstName",
                    LastName = "DefaultLastName",
                    MongoUserId = ObjectId.GenerateNewId().ToString(), 
                    CreatedAt = DateTime.UtcNow,
                    Role = userRole
                };



                var result = await _userManager.CreateAsync(identityUser);

                if (!result.Succeeded)
                {
                    return BadRequest("Failed to create user.");
                }

                // Assign the user to the appropriate role
                await _userManager.AddToRoleAsync(identityUser, userRole);

                // Step 2: Assign the user to a course
                var course = _context.Courses.FirstOrDefault(c => c.Name == "Math 101");
                if (course != null)
                {
                    _context.UserCourses.Add(new UserCourse
                    {
                        UserId = identityUser.Id,
                        CourseId = course.Id
                    });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Handle pre-existing users without a role
                if (string.IsNullOrEmpty(identityUser.Role))
                {
                    identityUser.Role = userRole; // Assign the role based on the LTI request
                    var updateResult = await _userManager.UpdateAsync(identityUser);

                    if (!updateResult.Succeeded)
                    {
                        return BadRequest("Failed to update user role.");
                    }
                }
            }

            // Log the user in
            await _signInManager.SignInAsync(identityUser, isPersistent: false);

            return View("LtiLaunch", ltiRequest);
        }


    }
}

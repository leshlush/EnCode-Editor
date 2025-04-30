using LtiLibrary.NetCore.Lti.v1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using MongoDB.Driver;

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

            // Add all form parameters to the LtiRequest
            foreach (var kvp in form)
            {
                ltiRequest.AddParameter(kvp.Key, kvp.Value);
            }

            // Check if the user exists in the AspNetUsers table
            var identityUser = await _userManager.FindByNameAsync(ltiRequest.UserId);

            if (identityUser == null)
            {
                // Step 1: Create a MongoDB user
                var mongoUser = new User
                {
                    Username = ltiRequest.UserId,
                    Email = $"{ltiRequest.UserId}@example.com",
                    CreatedAt = DateTime.UtcNow
                };
                await _mongoDb.Users.InsertOneAsync(mongoUser);

                // Step 2: Create an Identity user with the MongoUserId
                identityUser = new AppUser
                {
                    UserName = ltiRequest.UserId,
                    Email = $"{ltiRequest.UserId}@example.com",
                    FirstName = "DefaultFirstName", // Replace with actual data if available
                    LastName = "DefaultLastName",   // Replace with actual data if available
                    MongoUserId = mongoUser.Id,     // Link to the MongoDB user
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(identityUser);

                if (!result.Succeeded)
                {
                    // Clean up MongoDB user if Identity creation fails
                    await _mongoDb.Users.DeleteOneAsync(u => u.Id == mongoUser.Id);

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                    return BadRequest("Failed to create user.");
                }
            }

            // Check if the user exists in the LtiUsers table
            var ltiUser = _context.LtiUsers.FirstOrDefault(u => u.UserId == identityUser.Id);

            if (ltiUser == null)
            {
                // Step 3: Create an entry in the LtiUsers table
                ltiUser = new LtiUser
                {
                    UserId = identityUser.Id, // Link to the AppUser.Id
                    ResourceLinkId = ltiRequest.ResourceLinkId,
                    Roles = ltiRequest.Roles,
                    ContextId = ltiRequest.ContextId,
                    ContextTitle = ltiRequest.ContextTitle,
                    ContextLabel = ltiRequest.ContextLabel,
                    ToolConsumerInstanceGuid = ltiRequest.ToolConsumerInstanceGuid,
                    ToolConsumerInstanceName = ltiRequest.ToolConsumerInstanceName
                };

                _context.LtiUsers.Add(ltiUser);
                await _context.SaveChangesAsync();
            }

            // Log the user in
            await _signInManager.SignInAsync(identityUser, isPersistent: false);

            // Respond with the appropriate content
            return View("LtiLaunch", ltiRequest);
        }

    }
}

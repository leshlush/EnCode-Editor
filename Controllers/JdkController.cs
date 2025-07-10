using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;
using Newtonsoft.Json;

namespace SnapSaves.Controllers
{
    public class JdkController : Controller
    {
        private readonly MongoDbContext _dbContext;

        public JdkController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Demo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string projectId, string userId, string projectName)
        {
            // Get the authenticated user's MongoUserId from claims
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(currentUserId))
                return View("JdkError", "You do not have access to this project.");

            // Only allow access to own projects
            if (userId != currentUserId)
                return View("JdkError", "You do not have access to this project.");

            // Fetch the project for the current user
            var project = await _dbContext.Projects
                .Find(p => p.Id == projectId && p.UserId == currentUserId)
                .FirstOrDefaultAsync();

            if (project == null)
                return View("JdkError", "You do not have access to this project.");


            if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(userId))
            {
                return View("JdkError", "You do not have access to this project.");
            }

          foreach (var file in project.Files)
            {
                if (file.IsBinary && !IsBase64String(file.Content))
                {
                    // Convert to base64 if not already
                    var bytes = System.Text.Encoding.UTF8.GetBytes(file.Content);
                    file.Content = Convert.ToBase64String(bytes);
                }
            }


            // Serialize the project to JSON
            var projectJson = JsonConvert.SerializeObject(project, new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });

            // Pass the JSON string to the view
            ViewData["ProjectJson"] = projectJson;
            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = project.UserId;
            ViewData["ProjectName"] = project.Name;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReadOnly(string projectId, string? shareToken = null)
        {
            // Fetch the project from MongoDB
            var project = await _dbContext.Projects
                .Find(p => p.Id == projectId)
                .FirstOrDefaultAsync();

            if (project == null)
                return View("JdkError", "Project not found or you do not have access to it.");

            // Check if user is logged in and is the owner
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (!string.IsNullOrEmpty(currentUserId) && project.UserId == currentUserId)
            {
                // Owner: show normal (editable) view
                return await Index(projectId, currentUserId, project.Name);
            }

            // (Optional) Validate shareToken here if you want to restrict anonymous access

           
            foreach (var file in project.Files)
            {
                if (file.IsBinary && !IsBase64String(file.Content))
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(file.Content);
                    file.Content = Convert.ToBase64String(bytes);
                }
            }

            var projectJson = JsonConvert.SerializeObject(project, new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });

            ViewData["ProjectJson"] = projectJson;
            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = project.UserId;
            ViewData["ProjectName"] = project.Name;
            ViewData["ReadOnly"] = true; 

            return View("Index");
        }


        private static bool IsBase64String(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            // Remove whitespace and check length
            s = s.Trim();
            if (s.Length % 4 != 0)
                return false;

            try
            {
                Convert.FromBase64String(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

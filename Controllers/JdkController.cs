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
            if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Project ID or User ID is missing.");
            }

            // Fetch the project from MongoDB
            var project = await _dbContext.Projects
                .Find(p => p.Id == projectId && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound("Project not found or you do not have access to it.");
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

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
        public async Task<IActionResult> Index(string projectId, string userId)
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

            // Serialize the project to JSON
            var projectJson = JsonConvert.SerializeObject(project, new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            });

            // Pass the JSON string to the view
            ViewData["ProjectJson"] = projectJson;
            ViewData["ProjectId"] = projectId;

            return View();
        }
    }
}

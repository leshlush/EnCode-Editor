using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly MongoDbContext _dbContext;

        public ProjectsController(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var projects = await _dbContext.Projects
                        .Find(p => p.UserId == userId)
                        .ToListAsync();

                    return View(projects);
                }
            }

            return View(new List<Project>());
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    // Create a new project
                    var newProject = new Project
                    {
                        Name = "New Project " + DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                        UserId = userId,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        Files = new List<ProjectFile>
                {
                    new ProjectFile
                    {
                        Path = "default.txt",
                        Content = "This is a default file.",
                        IsDirectory = false
                    }
                }
                    };

                    // Insert the new project into the database
                    await _dbContext.Projects.InsertOneAsync(newProject);

                    // Redirect back to the Projects page
                    return RedirectToAction(nameof(Index));
                }
            }

            return Unauthorized();
        }
    }
}
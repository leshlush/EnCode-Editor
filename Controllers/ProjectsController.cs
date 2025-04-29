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

                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }

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

                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }

                if (!User.Identity?.IsAuthenticated ?? false)
                {
                    Console.WriteLine("User is not authenticated");
                    return Unauthorized();
                }

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

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized("User is not authenticated");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("MongoUserId claim is missing");
            }

            // Fetch the project by ID
            var project = await _dbContext.Projects.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound("Project not found");
            }

            // Ensure the project belongs to the logged-in user
            if (project.UserId != userId)
            {
                return Forbid("You do not have access to this project");
            }

            return View(project);
        }

    }
}
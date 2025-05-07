using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
                    // Validate that userId is a valid ObjectId
                    if (!ObjectId.TryParse(userId, out _))
                    {
                        Console.WriteLine($"Invalid MongoUserId: {userId}");
                        return BadRequest("Invalid MongoUserId format.");
                    }

                    // Fetch user projects
                    var userProjects = await _dbContext.Projects
                        .Find(p => p.UserId == userId)
                        .ToListAsync();

                    // Fetch template projects
                    var templateProjects = await _dbContext.TemplateProjects
                        .Find(_ => true) // Fetch all templates
                        .ToListAsync();

                    // Pass both user projects and templates to the view
                    ViewData["Templates"] = templateProjects;
                    return View(userProjects);
                }
            }

            return View(new List<Project>());
        }




        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }


            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("MongoUserId claim is missing");
            }

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

            await _dbContext.Projects.InsertOneAsync(newProject);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CopyTemplate(string templateId)
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

            // Fetch the template project
            var template = await _dbContext.TemplateProjects.Find(t => t.Id == templateId).FirstOrDefaultAsync();

            if (template == null)
            {
                return NotFound("Template project not found");
            }

            // Create a copy of the template for the user
            var newProject = new Project
            {
                Name = template.Name + " (Copy)",
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = template.Files.Select(f => new ProjectFile
                {
                    Path = f.Path,
                    Content = f.Content,
                    IsDirectory = f.IsDirectory
                }).ToList()
            };

            // Insert the new project into the user's projects collection
            await _dbContext.Projects.InsertOneAsync(newProject);

            return RedirectToAction(nameof(Index));
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

            // Check if the request is for JSON data
            if (Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                return Json(project);
            }

            return View(project);
        }

    }
}
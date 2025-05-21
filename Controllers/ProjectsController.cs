using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;
using Microsoft.EntityFrameworkCore;

namespace SnapSaves.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly MongoDbContext _dbContext;
        private readonly AppIdentityDbContext _identityDbContext;
        private readonly ProjectHelper _projectHelper;
        public ProjectsController(MongoDbContext dbContext, AppIdentityDbContext identityDbContext,
                ProjectHelper projectHelper)
        {
            _dbContext = dbContext;
            _identityDbContext = identityDbContext;
            _projectHelper = projectHelper;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated");

            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("MongoUserId claim is missing");

            // Only allow deleting user's own projects
            var result = await _dbContext.Projects.DeleteOneAsync(p => p.Id == id && p.UserId == userId);

            if (result.DeletedCount == 0)
                return NotFound("Project not found or you do not have permission to delete it.");

            // Remove the associated ProjectRecord from MySQL
            var projectRecord = await _identityDbContext.ProjectRecords
                .FirstOrDefaultAsync(r => r.MongoId == id && r.UserId == userId);
            if (projectRecord != null)
            {
                _identityDbContext.ProjectRecords.Remove(projectRecord);
                await _identityDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromUniversalTemplate(string templateId)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated");

            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("MongoUserId claim is missing");

            // Fetch the Template from SQL to get the InstructionsId and MongoId
            var template = _identityDbContext.Templates.FirstOrDefault(t => t.MongoId == templateId);
            if (template == null)
                return NotFound("Template not found");

            var result = await _projectHelper.CreateProjectFromTemplateAsync(template, userId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return RedirectToAction(nameof(Index));
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
            var projectRecord = new ProjectRecord
            {
                MongoId = newProject.Id,
                UserId = userId,
                CourseId = null, // Set if you have a course context
                CreatedAt = newProject.CreatedAt
            };
            _identityDbContext.ProjectRecords.Add(projectRecord);
            await _identityDbContext.SaveChangesAsync();
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
            var projectRecord = new ProjectRecord
            {
                MongoId = newProject.Id,
                UserId = userId,
                CourseId = null, // Set if you have a course context
                CreatedAt = newProject.CreatedAt
            };
            _identityDbContext.ProjectRecords.Add(projectRecord);
            await _identityDbContext.SaveChangesAsync();
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromTemplateWithName(int templateId, int courseId, string projectName)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("MongoUserId claim is missing");

            var template = _identityDbContext.Templates.FirstOrDefault(t => t.Id == templateId);
            if (template == null)
                return NotFound("Template not found");

            // Use ProjectHelper to create the project with a custom name
            var result = await _projectHelper.CreateProjectFromTemplateAsync(template, userId, projectName);
            if (!result.Success || result.Project == null)
                return BadRequest(result.ErrorMessage);

            var projectRecord = new ProjectRecord
            {
                MongoId = result.Project.Id,
                UserId = userId,
                CourseId = courseId, // Use the courseId from the form
                CreatedAt = result.Project.CreatedAt
            };
            _identityDbContext.ProjectRecords.Add(projectRecord);

            // Insert into TemplateProjects table
            var templateProject = new TemplateProject
            {
                TemplateId = templateId,
                ProjectMongoId = result.Project.Id
            };
            _identityDbContext.TemplateProjects.Add(templateProject);
            await _identityDbContext.SaveChangesAsync();

            // Optionally, return a partial or JSON for AJAX
            return Ok();
        }


        public async Task<(bool Success, string ErrorMessage, Project? Project)> CreateProjectFromTemplateAsync(
    Template template, string userId, string? customName = null)
        {
            // Fetch the template project from MongoDB
            var templateProject = await _dbContext.TemplateProjects.Find(t => t.Id == template.MongoId).FirstOrDefaultAsync();
            if (templateProject == null)
                return (false, "Template project not found in MongoDB.", null);

            // Create a new project for the user
            var newProject = new Project
            {
                Name = string.IsNullOrWhiteSpace(customName) ? template.Name + " (Copy)" : customName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Files = templateProject.Files.Select(f => new ProjectFile
                {
                    Path = f.Path,
                    Content = f.Content,
                    IsDirectory = f.IsDirectory
                }).ToList(),
                InstructionsId = template.InstructionsId
            };

            await _dbContext.Projects.InsertOneAsync(newProject);

            return (true, "", newProject);
        }



    }
}
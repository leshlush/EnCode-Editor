using Microsoft.AspNetCore.Mvc;
using SnapSaves.Models;
using SnapSaves.Data;
using MongoDB.Driver;

namespace SnapSaves.Controllers
{
    public class SnapCodeController : Controller
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly MongoDbContext _mongoDbContext;

        public SnapCodeController(AppIdentityDbContext dbContext, MongoDbContext mongoDbContext)
        {
            _dbContext = dbContext;
            _mongoDbContext = mongoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string projectId, string userId)
        {
            // Load the project from MongoDB
            var project = await _mongoDbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return NotFound();

            Instructions? instructions = null;
            string? instructionsPath = null;

            if (!string.IsNullOrEmpty(project.InstructionsId))
            {
                instructions = await _dbContext.Instructions.FindAsync(project.InstructionsId);
                if (instructions != null && instructions.Type == InstructionsType.Static)
                {
                    instructionsPath = "/" + instructions.Location.Replace("\\", "/");
                }
                project.Instructions = instructions;
            }

            if (!string.IsNullOrEmpty(instructionsPath))
            {
                ViewData["ShowInstructionsButtons"] = true;
                // Pass the path to the view for rendering the instructions pane
                ViewData["InstructionsPath"] = instructionsPath;
                ViewData["ProjectId"] = projectId;
                ViewData["UserId"] = userId;
                ViewData["ProjectName"] = project.Name;
                return View("ProjectWithInstructions", project);
            }
            else
            {
                ViewData["ShowInstructionsButtons"] = false;
                // No instructions: show a view without the instructions pane
                ViewData["ProjectId"] = projectId;
                ViewData["UserId"] = userId;
                ViewData["ProjectName"] = project.Name;
                return View("Index", project);
            }
        }
    }
}

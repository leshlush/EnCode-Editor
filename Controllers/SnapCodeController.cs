using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Models;
using SnapSaves.Data;
using SnapSaves.Helpers;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace SnapSaves.Controllers
{
    public class SnapCodeController : Controller
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly MongoDbContext _mongoDbContext;
        private readonly PermissionHelper _permissionHelper;
        private readonly UserManager<AppUser> _userManager;

        public SnapCodeController(
            AppIdentityDbContext dbContext,
            MongoDbContext mongoDbContext,
            PermissionHelper permissionHelper,
            UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _mongoDbContext = mongoDbContext;
            _permissionHelper = permissionHelper;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> ReadOnly(string projectId, string? shareToken = null)
        {
            // Fetch the project from MongoDB
            var project = await _mongoDbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return NotFound();

            // Try to get instructions (if any)
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

            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = project.UserId;
            ViewData["ProjectName"] = project.Name;
            ViewData["IsReadOnly"] = true;
            ViewData["ShareToken"] = shareToken;

            if (!string.IsNullOrEmpty(instructionsPath))
            {
                ViewData["ShowInstructionsButtons"] = true;
                ViewData["InstructionsPath"] = instructionsPath;
                return View("ProjectWithInstructions", project);
            }
            else
            {
                ViewData["ShowInstructionsButtons"] = false;
                return View("Index", project);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> StudentProject(string projectId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Forbid();

            // Check permission
            if (!await _permissionHelper.UserHasPermissionAsync(currentUser, "ViewStudentProjects"))
                return Forbid();

            // Get ProjectRecord for this project
            var projectRecord = await _dbContext.ProjectRecords
                .FirstOrDefaultAsync(pr => pr.MongoId == projectId);

            if (projectRecord == null || projectRecord.CourseId == null)
                return NotFound("Project or course not found.");

            // Check if current user is in the same course (as teacher/manager)
            var userCourse = await _dbContext.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == currentUser.Id && uc.CourseId == projectRecord.CourseId);

            if (userCourse == null)
                return Forbid();

            // Fetch the project from MongoDB
            var project = await _mongoDbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return NotFound("Project not found.");

            // Prepare view data for read-only view
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

            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = project.UserId;
            ViewData["ProjectName"] = project.Name;
            ViewData["IsReadOnly"] = true;

            if (!string.IsNullOrEmpty(instructionsPath))
            {
                ViewData["ShowInstructionsButtons"] = true;
                ViewData["InstructionsPath"] = instructionsPath;
                return View("ProjectWithInstructions", project);
            }
            else
            {
                ViewData["ShowInstructionsButtons"] = false;
                return View("Index", project);
            }
        }
    }
}

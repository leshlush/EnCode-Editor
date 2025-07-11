using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Models;
using SnapSaves.Data;
using SnapSaves.Helpers;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SnapSaves.Controllers
{
    public class JdkController : Controller
    {
        private readonly MongoDbContext _dbContext;
        private readonly AppIdentityDbContext _identityDbContext;
        private readonly PermissionHelper _permissionHelper;
        private readonly UserManager<AppUser> _userManager;

        public JdkController(
            MongoDbContext dbContext,
            AppIdentityDbContext identityDbContext,
            PermissionHelper permissionHelper,
            UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _identityDbContext = identityDbContext;
            _permissionHelper = permissionHelper;
            _userManager = userManager;
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
            if (!string.IsNullOrEmpty(shareToken))
            {
                var shareLink = await _identityDbContext.ProjectShareLinks
                    .FirstOrDefaultAsync(l => l.Token == shareToken && l.ProjectMongoId == projectId && l.IsActive);
                if (shareLink == null)
                    return View("JdkError", "Invalid or expired share link.");
            }

            // Fetch the project from MongoDB
            var project = await _dbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return View("JdkError", "Project not found or you do not have access to it.");

            // Owner check (optional)
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (!string.IsNullOrEmpty(currentUserId) && project.UserId == currentUserId)
            {
                return await Index(projectId, currentUserId, project.Name);
            }

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
            var projectRecord = await _identityDbContext.ProjectRecords
                .FirstOrDefaultAsync(pr => pr.MongoId == projectId);

            if (projectRecord == null || projectRecord.CourseId == null)
                return NotFound("Project or course not found.");

            // Check if current user is in the same course (as teacher/manager)
            var userCourse = await _identityDbContext.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == currentUser.Id && uc.CourseId == projectRecord.CourseId);

            if (userCourse == null)
                return Forbid();

            // Fetch the project from MongoDB
            var project = await _dbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return NotFound("Project not found.");

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

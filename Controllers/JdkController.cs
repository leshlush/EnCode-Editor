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
        public IActionResult Demo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string projectId, string userId, string projectName)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(currentUserId) || userId != currentUserId)
                return View("JdkError", "You do not have access to this project.");

            var project = await GetProjectForUserAsync(projectId, currentUserId);
            if (project == null)
                return View("JdkError", "You do not have access to this project.");

            PrepareProjectForView(project, projectId, projectName, isReadOnly: false);
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

            var project = await GetProjectAsync(projectId);
            if (project == null)
                return View("JdkError", "Project not found or you do not have access to it.");

            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            // In JdkController, ReadOnly action:
            if (!string.IsNullOrEmpty(currentUserId) && project.UserId == currentUserId)
                return RedirectToAction("Index", new { projectId = projectId, userId = currentUserId, projectName = project.Name });

            PrepareProjectForView(project, projectId, project.Name, isReadOnly: true);
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> StudentProject(string projectId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Forbid();

            if (!await _permissionHelper.UserHasPermissionAsync(currentUser, "ViewStudentProjects"))
                return Forbid();

            var projectRecord = await _identityDbContext.ProjectRecords
                .FirstOrDefaultAsync(pr => pr.MongoId == projectId);

            if (projectRecord == null || projectRecord.CourseId == null)
                return NotFound("Project or course not found.");

            var userCourse = await _identityDbContext.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == currentUser.Id && uc.CourseId == projectRecord.CourseId);

            if (userCourse == null)
                return Forbid();

            var project = await GetProjectAsync(projectId);
            if (project == null)
                return NotFound("Project not found.");

            PrepareProjectForView(project, projectId, project.Name, isReadOnly: true);
            return View("Index");
        }

        // --- Helper Methods ---

        private async Task<Project?> GetProjectAsync(string projectId)
        {
            return await _dbContext.Projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();
        }

        private async Task<Project?> GetProjectForUserAsync(string projectId, string userId)
        {
            return await _dbContext.Projects.Find(p => p.Id == projectId && p.UserId == userId).FirstOrDefaultAsync();
        }

        private void PrepareProjectForView(Project project, string projectId, string projectName, bool isReadOnly)
        {
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
            ViewData["ProjectName"] = projectName;
            ViewData["ReadOnly"] = isReadOnly;
        }

        private static bool IsBase64String(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;
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
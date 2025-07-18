using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using MongoDB.Driver;
using SnapSaves.Helpers;

namespace SnapSaves.Controllers
{
    [Authorize] // Restrict access to logged-in users
    public class CoursesController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly MongoDbContext _mongoDbContext;

        public CoursesController(AppIdentityDbContext context, UserManager<AppUser> userManager, MongoDbContext mongoDbContext)
        {
            _context = context;
            _userManager = userManager;
            _mongoDbContext = mongoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            // Fetch the courses the user is enrolled in
            var courses = await _context.UserCourses
                .Where(uc => uc.UserId == currentUser.Id)
                .Include(uc => uc.Course)
                .Select(uc => uc.Course)
                .ToListAsync();

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Fetch the course with its related data
            var course = await _context.Courses
                .Include(c => c.CourseTemplates)
                .ThenInclude(ct => ct.Template)
                .Include(c => c.UserCourses)
                .ThenInclude(uc => uc.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Fetch all universal templates (not tied to the course)
            var universalTemplates = await _context.Templates
                .Where(t => t.IsUniversal == true)
                .ToListAsync();

            // Ensure IsUniversal is false for templates with null values
            foreach (var template in course.CourseTemplates.Select(ct => ct.Template))
            {
                template.IsUniversal ??= false;
            }

            // Add universal templates to the course's UniversalTemplates property
            course.UniversalTemplates = universalTemplates;

            // Use the LearningPathHelper to fetch learning paths
            var learningPathHelper = new LearningPathHelper(_context);
            var learningPaths = await learningPathHelper.GetLearningPathsWithItemsAsync();

            ViewData["LearningPaths"] = learningPaths;

            return View(course);
        }

        [Authorize]
        public async Task<PartialViewResult> CourseProjects(int id)
        {
            // id is the CourseId
            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return PartialView("_CourseProjects", Enumerable.Empty<Project>());

            // Get all ProjectRecords for this user and course
            var projectRecords = await _context.ProjectRecords
                .Where(pr => pr.UserId == userId && pr.CourseId == id)
                .ToListAsync();

            var mongoIds = projectRecords.Select(pr => pr.MongoId).ToList();

            // Fetch the actual projects from MongoDB
            var projects = await _mongoDbContext.Projects
                .Find(p => mongoIds.Contains(p.Id))
                .ToListAsync();

            // Order by most recently updated
            projects = projects.OrderByDescending(p => p.LastModified).ToList();
            ViewData["CourseId"] = id;
            return PartialView("_CourseProjects", projects);
        }

    }
}

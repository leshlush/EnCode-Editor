using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Authorize] // Restrict access to logged-in users
    public class CoursesController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CoursesController(AppIdentityDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            // Fetch the course by ID
            var course = await _context.Courses
                .Include(c => c.UserCourses)
                .ThenInclude(uc => uc.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}

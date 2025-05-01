using Microsoft.AspNetCore.Mvc;
using global::SnapSaves.Auth;
using global::SnapSaves.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{


    
        [Authorize(Roles = "Teacher")]
        public class StudentsController : Controller
        {
            private readonly AppIdentityDbContext _context;
            private readonly UserManager<AppUser> _userManager;

            public StudentsController(AppIdentityDbContext context, UserManager<AppUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                // Get the current teacher
                var currentUser = await _userManager.GetUserAsync(User);

                // Get the courses the teacher is associated with
                var teacherCourses = await _context.UserCourses
                    .Where(uc => uc.UserId == currentUser.Id)
                    .Select(uc => uc.CourseId)
                    .ToListAsync();

                // Get all students in the teacher's courses
                var students = await _context.UserCourses
                    .Where(uc => teacherCourses.Contains(uc.CourseId) && uc.User.Role == "Student")
                    .Include(uc => uc.User)
                    .Select(uc => uc.User)
                    .ToListAsync();

                return View(students);
            }
        }
    

}

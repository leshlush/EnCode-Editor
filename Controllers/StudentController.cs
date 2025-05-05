using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class StudentsController : Controller
{
    private readonly AppIdentityDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly PermissionHelper _permissionHelper;

    public StudentsController(AppIdentityDbContext context, UserManager<AppUser> userManager, PermissionHelper permissionHelper)
    {
        _context = context;
        _userManager = userManager;
        _permissionHelper = permissionHelper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null || !await _permissionHelper.UserHasPermissionAsync(currentUser, "ViewStudents"))
        {
            return Forbid();
        }

        // Get the courses the teacher is associated with
        var teacherCourses = await _context.UserCourses
            .Where(uc => uc.UserId == currentUser.Id)
            .Select(uc => uc.CourseId)
            .ToListAsync();

        // Get all students in the teacher's courses
        var students = await _context.UserCourses
            .Where(uc => teacherCourses.Contains(uc.CourseId))
            .Include(uc => uc.User)
            .Select(uc => uc.User)
            .ToListAsync();

        return View(students);
    }
}

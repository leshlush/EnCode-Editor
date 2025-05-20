using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Helpers;
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

        // Ensure the user is logged in
        if (currentUser == null)
        {
            return Forbid();
        }

        // Check if the user has the "ViewStudents" permission
        if (!await _permissionHelper.UserHasPermissionAsync(currentUser, "ViewStudents"))
        {
            return Forbid();
        }

        // Get the courses the user is associated with
        var userCourses = await _context.UserCourses
            .Where(uc => uc.UserId == currentUser.Id)
            .Select(uc => uc.CourseId)
            .ToListAsync();

        // Get all users in the courses who have the "Student" role
        var students = await _context.UserCourses
            .Where(uc => userCourses.Contains(uc.CourseId))
            .Include(uc => uc.User)
            .Select(uc => uc.User)
            .Where(u => _context.UserRoles
                .Any(ur => ur.UserId == u.Id && _context.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Student")))
            .ToListAsync();


        return View(students);
    }

}

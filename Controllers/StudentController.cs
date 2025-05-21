using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Helpers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

[Authorize]
public class StudentsController : Controller
{
    private readonly AppIdentityDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly PermissionHelper _permissionHelper;
    private readonly MongoDbContext _mongoDbContext;

    public StudentsController(AppIdentityDbContext context, UserManager<AppUser> userManager, 
        PermissionHelper permissionHelper, MongoDbContext mongoDbContext)
    {
        _context = context;
        _userManager = userManager;
        _permissionHelper = permissionHelper;
        _mongoDbContext = mongoDbContext;
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> StudentProjectsPartial(string userId, int courseId)
    {
        // Get all ProjectRecords for this user and course
        var projectMongoIds = await _context.ProjectRecords
            .Where(pr => pr.UserId == userId && pr.CourseId == courseId)
            .Select(pr => pr.MongoId)
            .ToListAsync();

        // Fetch projects from MongoDB
        var userProjects = await _mongoDbContext.Projects
            .Find(p => projectMongoIds.Contains(p.Id))
            .ToListAsync();

        return PartialView("_StudentProjectsPartial", userProjects);
    }


}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Helpers
{
    public class DatabaseSeeder
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserHelper _userHelper;
        private readonly TemplateHelper _templateHelper;

        public DatabaseSeeder(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UserHelper userHelper,
            TemplateHelper templateHelper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userHelper = userHelper;
            _templateHelper = templateHelper;
        }

        public async Task SeedAsync()
        {
            // Apply migrations
            await _context.Database.MigrateAsync();

            // Seed roles
            await SeedRolesAsync();

            // Seed courses
            var courses = await SeedCoursesAsync();

            // Seed teacher
            await SeedTeacherAsync(courses);

            // Seed students
            await SeedStudentsAsync(courses);

            // Seed templates
            await SeedTemplatesAsync(courses);
        }

        private async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Teacher"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Teacher"));
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }

            // Seed permissions
            var permissions = new List<Permission>
    {
        new Permission { Name = "ViewCourses" },
        new Permission { Name = "EditCourses" },
        new Permission { Name = "ViewStudents" }
    };

            foreach (var permission in permissions)
            {
                if (!_context.Permissions.Any(p => p.Name == permission.Name))
                {
                    _context.Permissions.Add(permission);
                }
            }
            await _context.SaveChangesAsync();

            // Assign permissions to roles
            var teacherRole = await _roleManager.FindByNameAsync("Teacher");
            var studentRole = await _roleManager.FindByNameAsync("Student");

            var teacherPermissions = new[] { "ViewCourses", "EditCourses", "ViewStudents" };
            var studentPermissions = new[] { "ViewCourses" };

            foreach (var permissionName in teacherPermissions)
            {
                var permission = _context.Permissions.FirstOrDefault(p => p.Name == permissionName);
                if (permission != null && !_context.RolePermissions.Any(rp => rp.RoleId == teacherRole.Id && rp.PermissionId == permission.Id))
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = teacherRole.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            foreach (var permissionName in studentPermissions)
            {
                var permission = _context.Permissions.FirstOrDefault(p => p.Name == permissionName);
                if (permission != null && !_context.RolePermissions.Any(rp => rp.RoleId == studentRole.Id && rp.PermissionId == permission.Id))
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = studentRole.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
        }


        private async Task<List<Course>> SeedCoursesAsync()
        {
            var courses = new List<Course>
            {
                new Course { Name = "Course 1", Description = "Description for Course 1" },
                new Course { Name = "Course 2", Description = "Description for Course 2" },
                new Course { Name = "Course 3", Description = "Description for Course 3" }
            };

            foreach (var course in courses)
            {
                if (!_context.Courses.Any(c => c.Name == course.Name))
                {
                    _context.Courses.Add(course);
                }
            }
            await _context.SaveChangesAsync();

            return await _context.Courses.ToListAsync();
        }

        private async Task SeedTeacherAsync(List<Course> courses)
        {
            var teacherEmail = "teacher@encodecreate.com";
            var teacherPassword = "Terrap1n";

            var (teacherSuccess, teacherError) = await _userHelper.CreateUserAsync(
                teacherEmail,
                teacherPassword,
                "Teacher",
                "EncodeCreate",
                "Teacher"
            );

            if (!teacherSuccess)
            {
                Console.WriteLine($"Failed to create teacher: {teacherError}");
                return;
            }

            var teacher = await _userManager.FindByEmailAsync(teacherEmail);
            foreach (var course in courses)
            {
                if (!_context.UserCourses.Any(uc => uc.UserId == teacher.Id && uc.CourseId == course.Id))
                {
                    _context.UserCourses.Add(new UserCourse
                    {
                        UserId = teacher.Id,
                        CourseId = course.Id
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedStudentsAsync(List<Course> courses)
        {
            for (int i = 1; i <= 15; i++)
            {
                var studentEmail = $"student{i}@encodecreate.com";
                var studentPassword = $"Password{i}!";

                var (studentSuccess, studentError) = await _userHelper.CreateUserAsync(
                    studentEmail,
                    studentPassword,
                    $"Student{i}",
                    "EncodeCreate",
                    "Student"
                );

                if (!studentSuccess)
                {
                    Console.WriteLine($"Failed to create student {i}: {studentError}");
                    continue;
                }

                var student = await _userManager.FindByEmailAsync(studentEmail);
                var courseIndex = (i - 1) / 5; // 0 for Course 1, 1 for Course 2, 2 for Course 3
                var course = courses[courseIndex];

                if (!_context.UserCourses.Any(uc => uc.UserId == student.Id && uc.CourseId == course.Id))
                {
                    _context.UserCourses.Add(new UserCourse
                    {
                        UserId = student.Id,
                        CourseId = course.Id
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedTemplatesAsync(List<Course> courses)
        {
            var templates = new List<(string Name, string Description)>
    {
        ("Template 1", "Description for Template 1"),
        ("Template 2", "Description for Template 2"),
        ("Template 3", "Description for Template 3"),
        ("Template 4", "Description for Template 4"),
        ("Template 5", "Description for Template 5"),
        ("Template 6", "Description for Template 6"),
        ("Template 7", "Description for Template 7"),
        ("Template 8", "Description for Template 8"),
        ("Template 9", "Description for Template 9")
    };

            for (int i = 0; i < templates.Count; i++)
            {
                var course = courses[i / 3]; // Distribute templates across 3 courses

                // Create a new project object
                var project = new Project
                {
                    Name = templates[i].Name,
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    Files = new List<ProjectFile>
            {
                new ProjectFile
                {
                    Path = "index.html",
                    Content = $"<html><head><title>{templates[i].Name}</title></head><body><h1>{templates[i].Name}</h1></body></html>",
                    IsDirectory = false
                },
                new ProjectFile
                {
                    Path = "style.css",
                    Content = "body { font-family: Arial, sans-serif; }",
                    IsDirectory = false
                }
            }
                };

                // Use the new method to create a template from the project
                var (success, errorMessage) = await _templateHelper.CreateNewProjectTemplateAsync(project, course.Id);
                if (!success)
                {
                    Console.WriteLine($"Failed to create template '{templates[i].Name}' for course '{course.Name}': {errorMessage}");
                }
                else
                {
                    // Ensure the template is added to the database
                    var template = await _context.Templates.FirstOrDefaultAsync(t => t.MongoId == project.Id);
                    if (template == null)
                    {
                        Console.WriteLine($"Template '{templates[i].Name}' was not added to the database.");
                    }
                }
            }
        }

    }
}

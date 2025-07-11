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
        private readonly ProjectHelper _projectHelper;

        public DatabaseSeeder(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UserHelper userHelper,
            TemplateHelper templateHelper,
            ProjectHelper projectHelper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userHelper = userHelper;
            _templateHelper = templateHelper;
            _projectHelper = projectHelper;
        }

        public async Task SeedAsync()
        {
            // Apply migrations
            await _context.Database.MigrateAsync();

            // Seed the default organization
            var defaultOrganization = await SeedDefaultOrganizationAsync();

            // Seed roles and permissions
            await SeedRolesAsync();

            // Seed admin user
            await SeedAdminAsync(defaultOrganization);

            // Seed courses
            var courses = await SeedCoursesAsync(defaultOrganization);

            // Seed teacher
            await SeedTeacherAsync(defaultOrganization, courses);

            // Seed students
           await SeedStudentsAsync(defaultOrganization, courses);

            // Seed templates
            await SeedTemplatesAsync(courses);
        }

        private async Task<Organization> SeedDefaultOrganizationAsync()
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Name == "Default Organization");
            if (organization == null)
            {
                organization = new Organization
                {
                    Name = "Default Organization",
                    Description = "This is the default organization."
                };

                _context.Organizations.Add(organization);
                await _context.SaveChangesAsync();
            }

            return organization;
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
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            // Seed permissions
            var permissions = new List<Permission>
    {
        new Permission { Name = "ViewCourses" },
        new Permission { Name = "EditCourses" },
        new Permission { Name = "ViewStudents" },
        new Permission { Name = "ViewTeachers" },
        new Permission { Name = "ViewStudentProjects" }
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
            var managerRole = await _roleManager.FindByNameAsync("Manager"); 

            var teacherPermissions = new[] { "ViewCourses", "EditCourses", "ViewStudents", "ViewStudentProjects" };
            var studentPermissions = new[] { "ViewCourses" };
            var managerPermissions = new[] { "ViewCourses", "EditCourses", "ViewStudents", "ViewTeachers", "ViewStudentProjects" };

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

        private async Task SeedAdminAsync(Organization defaultOrganization)
        {
            var adminEmail = "admin@encodecreate.com";
            var adminPassword = "Admin123!";

            // Check if the admin user already exists
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // Create the admin user
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    MongoUserId = ObjectId.GenerateNewId().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    OrganizationId = defaultOrganization.Id
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return;
                }

                // Assign the "Admin" role to the user
                var roleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Failed to assign Admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
        private async Task<List<Course>> SeedCoursesAsync(Organization defaultOrganization)
        {
            var courses = new List<Course>
    {
        new Course { Name = "Course 1", Description = "Description for Course 1", OrganizationId = defaultOrganization.Id },
        new Course { Name = "Course 2", Description = "Description for Course 2", OrganizationId = defaultOrganization.Id },
        new Course { Name = "Course 3", Description = "Description for Course 3", OrganizationId = defaultOrganization.Id }
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

        private async Task SeedTeacherAsync(Organization defaultOrganization, List<Course> courses)
        {
            var teacherEmail = "teacher2@encodecreate.com";
            var teacherPassword = "Terrap1n";

            // Pass the OrganizationId to the UserHelper
            var (teacherSuccess, teacherError) = await _userHelper.CreateUserAsync(
                teacherEmail,
                teacherPassword,
                "Teacher",
                "EncodeCreate",
                "Teacher",
                defaultOrganization.Id // Pass the OrganizationId here
            );

            if (!teacherSuccess)
            {
                Console.WriteLine($"Failed to create teacher: {teacherError}");
                return;
            }

            var teacher = await _userManager.FindByEmailAsync(teacherEmail);
            if (teacher == null)
            {
                Console.WriteLine("Teacher user not found after creation.");
                return;
            }

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

            // Seed the teacher's project from the AppleCatchers folder
            await SeedTeacherProjectAsync(teacher);

            await _context.SaveChangesAsync();
        }

        private async Task SeedTeacherProjectAsync(AppUser teacher)
        {
            try
            {
                // Path to the AppleCatchers folder
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Assets", "AppleCatchers");

                // Ensure the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Directory '{directoryPath}' does not exist. Skipping teacher project seeding.");
                    return;
                }

                // Use ProjectHelper to create the project
                var project = await _projectHelper.CreateProjectFromDirectoryAsync(directoryPath, teacher.MongoUserId);

                Console.WriteLine($"Project '{project.Name}' created for teacher with ID: {teacher.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed teacher project: {ex.Message}");
            }
        }
    
        private async Task SeedStudentsAsync(Organization defaultOrganization, List<Course> courses)
        {
            for (int i = 1; i <= 15; i++)
            {
                var studentEmail = $"student{i}@encodecreate.com";
                var studentPassword = $"Password{i}!";

                // Pass the OrganizationId to the UserHelper
                var (studentSuccess, studentError) = await _userHelper.CreateUserAsync(
                    studentEmail,
                    studentPassword,
                    $"Student{i}",
                    "EncodeCreate",
                    "Student",
                    defaultOrganization.Id // Pass the OrganizationId here
                );

                if (!studentSuccess)
                {
                    Console.WriteLine($"Failed to create student {i}: {studentError}");
                    continue;
                }

                var student = await _userManager.FindByEmailAsync(studentEmail);
                if (student == null)
                {
                    Console.WriteLine($"Student {i} not found after creation.");
                    continue;
                }

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
            if (courses == null || courses.Count == 0)
            {
                Console.WriteLine("No courses found. Skipping template seeding.");
                return;
            }

            // 1. Get an admin user to use as the owner of the seed projects
            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "admin@encodecreate.com");
            if (adminUser == null)
            {
                Console.WriteLine("Admin user not found. Cannot seed templates.");
                return;
            }

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

                // Check if a template with the same name already exists for this course
                var existingTemplate = await _context.Templates
                    .Where(t => t.Name == templates[i].Name)
                    .Join(_context.CourseTemplates,
                        t => t.Id,
                        ct => ct.TemplateId,
                        (t, ct) => new { t, ct })
                    .Where(x => x.ct.CourseId == course.Id)
                    .Select(x => x.t)
                    .FirstOrDefaultAsync();

                if (existingTemplate != null)
                {
                    Console.WriteLine($"Template '{templates[i].Name}' already exists for course '{course.Name}'. Skipping.");
                    continue;
                }

                // 2. Set the UserId to the admin's MongoUserId
                var project = new Project
                {
                    Name = templates[i].Name,
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    UserId = adminUser.MongoUserId, // <-- This is the fix!
                    Files = new List<ProjectFile>
            {
                new ProjectFile
                {
                    Path = "index.html",
                    Content = $"<html><head><title>{templates[i].Name}</title></head><body><h1>{templates[i].Name}</h1></body></html>",
                    IsDirectory = false,
                    IsBinary = false
                },
                new ProjectFile
                {
                    Path = "style.css",
                    Content = "body { font-family: Arial, sans-serif; }",
                    IsDirectory = false,
                    IsBinary = false
                }
            }
                };

                // Use the helper to create a template from the project
                var (success, errorMessage) = await _templateHelper.CreateNewProjectTemplateAsync(project, course.Id);
                if (!success)
                {
                    Console.WriteLine($"Failed to create template '{templates[i].Name}' for course '{course.Name}': {errorMessage}");
                }
                else
                {
                    Console.WriteLine($"Template '{templates[i].Name}' created for course '{course.Name}'.");
                }
            }
        }



    }
}

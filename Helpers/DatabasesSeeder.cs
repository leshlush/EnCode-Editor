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
        private readonly TemplateHelper _templateHelper;
        private readonly LearningPathImporterHelper _learningPathImporterHelper;

        public DatabaseSeeder(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TemplateHelper templateHelper,
            LearningPathImporterHelper learningPathImporterHelper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _templateHelper = templateHelper;
            _learningPathImporterHelper = learningPathImporterHelper;
        }

        public async Task SeedAsync()
        {
            // Apply migrations
            await _context.Database.MigrateAsync();

            // Seed default organization
            var defaultOrganization = await SeedDefaultOrganizationAsync();

            // Seed roles and permissions
            await SeedRolesAndPermissionsAsync();

            // Seed admin user
            var adminUser = await SeedAdminUserAsync(defaultOrganization);

            // Seed courses
            var courses = await SeedCoursesAsync(defaultOrganization);

            // Seed templates
            await SeedTemplatesAsync(courses, adminUser);

            // Seed learning paths and items
            await SeedLearningPathsAsync();

            // Import learning paths from the directory
            await ImportLearningPathsAsync();

            // Seed teacher and students
            await SeedTeacherAsync(defaultOrganization, courses);
            await SeedStudentsAsync(defaultOrganization, courses);
        }

        private async Task ImportLearningPathsAsync()
        {
            // Define the root directory for learning paths
            var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "LearningPaths");

            // Call the LearningPathImporterHelper to import learning paths
            try
            {
                await _learningPathImporterHelper.ImportLearningPathsAsync(rootDirectory);
                Console.WriteLine("Learning paths imported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing learning paths: {ex.Message}");
            }
        }

        private async Task<Organization> SeedDefaultOrganizationAsync()
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Name == "Default Organization");
            if (organization == null)
            {
                organization = new Organization
                {
                    Name = "Default Organization",
                    Description = "This is the default organization.",
                    Type = OrganizationType.Default,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Organizations.Add(organization);
                await _context.SaveChangesAsync();
            }

            return organization;
        }

        private async Task SeedRolesAndPermissionsAsync()
        {
            var roles = new[] { "Admin", "Teacher", "Student" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var permissions = new[]
            {
                "ViewCourses", "EditCourses", "ViewStudents", "ViewTeachers", "ViewStudentProjects"
            };

            foreach (var permission in permissions)
            {
                if (!_context.Permissions.Any(p => p.Name == permission))
                {
                    _context.Permissions.Add(new Permission { Name = permission });
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task<AppUser> SeedAdminUserAsync(Organization organization)
        {
            var adminEmail = "admin@encodecreate.com";
            var adminPassword = "Admin123!";

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    MongoUserId = ObjectId.GenerateNewId().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    OrganizationId = organization.Id
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            return adminUser;
        }

        private async Task<List<Course>> SeedCoursesAsync(Organization organization)
        {
            var courses = new List<Course>
            {
                new Course { Name = "Course 1", Description = "Description for Course 1", OrganizationId = organization.Id },
                new Course { Name = "Course 2", Description = "Description for Course 2", OrganizationId = organization.Id },
                new Course { Name = "Course 3", Description = "Description for Course 3", OrganizationId = organization.Id }
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

        private async Task SeedTemplatesAsync(List<Course> courses, AppUser adminUser)
        {
            var templates = new List<(string Name, string Description)>
            {
                ("Template 1", "Description for Template 1"),
                ("Template 2", "Description for Template 2"),
                ("Template 3", "Description for Template 3")
            };

            foreach (var (name, description) in templates)
            {
                if (!_context.Templates.Any(t => t.Name == name))
                {
                    var project = new Project
                    {
                        Name = name,
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        UserId = adminUser.MongoUserId,
                        Files = new List<ProjectFile>
                        {
                            new ProjectFile { Path = "index.html", Content = $"<h1>{name}</h1>", IsDirectory = false },
                            new ProjectFile { Path = "style.css", Content = "body { font-family: Arial; }", IsDirectory = false }
                        }
                    };

                    var (success, errorMessage) = await _templateHelper.CreateNewProjectTemplateAsync(project, courses.First().Id);
                    if (!success)
                    {
                        throw new Exception($"Failed to create template '{name}': {errorMessage}");
                    }
                }
            }
        }

        private async Task SeedLearningPathsAsync()
        {
            if (!await _context.LearningPaths.AnyAsync())
            {
                var learningPath = new LearningPath
                {
                    Name = "Sample Learning Path",
                    Description = "A sample learning path for demonstration purposes."
                };

                _context.LearningPaths.Add(learningPath);
                await _context.SaveChangesAsync();

                // Create units with position ranges
                var units = new List<Unit>
                {
                    new Unit { Name = "Unit 1", StartPosition = 0, EndPosition = 4, LearningPathId = learningPath.Id },
                    new Unit { Name = "Unit 2", StartPosition = 5, EndPosition = 9, LearningPathId = learningPath.Id }
                };

                _context.AddRange(units);
                await _context.SaveChangesAsync();

                // Create LearningItems and associate them with the LearningPath
                for (int i = 0; i < 10; i++)
                {
                    var learningItem = new LearningItem
                    {
                        Name = $"Learning Item {i + 1}",
                        ItemType = LearningItemType.Template,
                        Position = i
                    };

                    _context.LearningItems.Add(learningItem);
                    await _context.SaveChangesAsync();

                    var learningPathItem = new LearningPathItem
                    {
                        LearningPathId = learningPath.Id,
                        LearningItemId = learningItem.Id
                    };

                    _context.LearningPathItems.Add(learningPathItem);
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedTeacherAsync(Organization organization, List<Course> courses)
        {
            var teacherEmail = "teacher@encodecreate.com";
            var teacherPassword = "Teacher123!";

            var teacher = await _userManager.FindByEmailAsync(teacherEmail);
            if (teacher == null)
            {
                teacher = new AppUser
                {
                    UserName = teacherEmail,
                    Email = teacherEmail,
                    FirstName = "Teacher",
                    LastName = "User",
                    MongoUserId = ObjectId.GenerateNewId().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    OrganizationId = organization.Id
                };

                var result = await _userManager.CreateAsync(teacher, teacherPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create teacher: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                await _userManager.AddToRoleAsync(teacher, "Teacher");
            }

            foreach (var course in courses)
            {
                if (!_context.UserCourses.Any(uc => uc.UserId == teacher.Id && uc.CourseId == course.Id))
                {
                    _context.UserCourses.Add(new UserCourse { UserId = teacher.Id, CourseId = course.Id });
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedStudentsAsync(Organization organization, List<Course> courses)
        {
            for (int i = 1; i <= 10; i++)
            {
                var studentEmail = $"student{i}@encodecreate.com";
                var studentPassword = $"Student{i}123!";

                var student = await _userManager.FindByEmailAsync(studentEmail);
                if (student == null)
                {
                    student = new AppUser
                    {
                        UserName = studentEmail,
                        Email = studentEmail,
                        FirstName = $"Student{i}",
                        LastName = "User",
                        MongoUserId = ObjectId.GenerateNewId().ToString(),
                        CreatedAt = DateTime.UtcNow,
                        OrganizationId = organization.Id
                    };

                    var result = await _userManager.CreateAsync(student, studentPassword);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create student {i}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }

                    await _userManager.AddToRoleAsync(student, "Student");
                }

                var course = courses[i % courses.Count];
                if (!_context.UserCourses.Any(uc => uc.UserId == student.Id && uc.CourseId == course.Id))
                {
                    _context.UserCourses.Add(new UserCourse { UserId = student.Id, CourseId = course.Id });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
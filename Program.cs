using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Identity
var mysqlConnection = builder.Configuration.GetConnectionString("MySQLIdentity");
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseMySql(
        mysqlConnection,
        ServerVersion.AutoDetect(mysqlConnection),
        mysqlOptions =>
        {
            mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
            mysqlOptions.EnableRetryOnFailure();
        }
    ));

// Configure Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+:";
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Configure MongoDB
builder.Services.AddSingleton<MongoDbContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".jar"] = "application/java-archive";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppIdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var mongoDbContext = services.GetRequiredService<MongoDbContext>();

    // Create an instance of UserHelper
    var userHelper = new UserHelper(mongoDbContext, userManager);

    // Apply migrations
    context.Database.Migrate();

    // Seed roles
    if (!await roleManager.RoleExistsAsync("Teacher"))
    {
        await roleManager.CreateAsync(new IdentityRole("Teacher"));
    }
    if (!await roleManager.RoleExistsAsync("Student"))
    {
        await roleManager.CreateAsync(new IdentityRole("Student"));
    }

    // Seed courses
    var courses = new List<Course>
{
    new Course { Name = "Course 1", Description = "Description for Course 1" },
    new Course { Name = "Course 2", Description = "Description for Course 2" },
    new Course { Name = "Course 3", Description = "Description for Course 3" }
};

    // Add courses to the database if they don't already exist
    foreach (var course in courses)
    {
        if (!context.Courses.Any(c => c.Name == course.Name))
        {
            context.Courses.Add(course);
        }
    }
    await context.SaveChangesAsync();

    // Query the courses back to ensure they are tracked by the DbContext
    var trackedCourses = await context.Courses.ToListAsync();

    // Seed teacher
    var teacherEmail = builder.Configuration["TeacherCredentials:Email"];
    var teacherPassword = builder.Configuration["TeacherCredentials:Password"];
    var (teacherSuccess, teacherError) = await userHelper.CreateUserAsync(
        teacherEmail,
        teacherPassword,
        "Teacher",
        "EncodeCreate",
        "Teacher"
    );

    if (!teacherSuccess)
    {
        Console.WriteLine($"Failed to create teacher: {teacherError}");
    }
    else
    {
        // Enroll teacher in all courses
        var teacher = await userManager.FindByEmailAsync(teacherEmail);
        foreach (var course in trackedCourses)
        {
            context.UserCourses.Add(new UserCourse
            {
                UserId = teacher.Id,
                CourseId = course.Id
            });
        }
        await context.SaveChangesAsync();
    }

    // Seed students
    for (int i = 1; i <= 15; i++)
    {
        var studentEmail = $"student{i}@encodecreate.com";
        var studentPassword = $"Password{i}!";
        var (studentSuccess, studentError) = await userHelper.CreateUserAsync(
            studentEmail,
            studentPassword,
            $"Student{i}",
            "EncodeCreate",
            "Student"
        );

        if (!studentSuccess)
        {
            Console.WriteLine($"Failed to create student {i}: {studentError}");
        }
        else
        {
            // Enroll students in courses (5 per course)
            var student = await userManager.FindByEmailAsync(studentEmail);
            var courseIndex = (i - 1) / 5; // 0 for Course 1, 1 for Course 2, 2 for Course 3
            var course = trackedCourses[courseIndex];
            context.UserCourses.Add(new UserCourse
            {
                UserId = student.Id,
                CourseId = course.Id
            });
        }
    }
    await context.SaveChangesAsync();

}

await app.RunAsync();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;
using Microsoft.AspNetCore.DataProtection;
using AspNetCore.DataProtection.Aws.S3;
using Amazon;


var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Identity
var mysqlConnection = builder.Configuration.GetConnectionString("MySQLIdentity");

Console.WriteLine($"[DEBUG] MySQL Connection String: {mysqlConnection}");

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
builder.Services.AddScoped<TemplateHelper>();
builder.Services.AddScoped<ProjectHelper>();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<PermissionHelper>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomUserClaimsPrincipalFactory>();
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/dataprotection-keys"));


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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
     //Resolve ProjectHelper and ProjectHelperTest
  //  var projectHelper = services.GetRequiredService<ProjectHelper>();
   //var projectHelperTest = new ProjectHelperTest(projectHelper);

    // Run the test
    //await projectHelperTest.TestCreateProjectFromDirectoryAsync();

    
    var databaseSeeder = services.GetRequiredService<DatabaseSeeder>();

    // Seed the database
     await databaseSeeder.SeedAsync();
}


await app.RunAsync();

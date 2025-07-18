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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;



var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Identity
var mysqlConnection = builder.Configuration.GetConnectionString("MySQLIdentity");
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104_857_600; // 100 MB
});


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

// Add Google OAuth authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] 
            ?? throw new InvalidOperationException("Google ClientId not configured");
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
            ?? throw new InvalidOperationException("Google ClientSecret not configured");
        
        // Ensure this matches your Google Console redirect URI
        options.CallbackPath = "/signin-google";
        
        // Request additional scopes for user information
        options.Scope.Add("profile");
        options.Scope.Add("email");
        
        // Save tokens for potential future API calls
        options.SaveTokens = true;
        
        // Map Google claims to Identity claims
        options.ClaimActions.MapJsonKey("given_name", "given_name");
        options.ClaimActions.MapJsonKey("family_name", "family_name");
        
        // Add error handling
        options.Events.OnRemoteFailure = context =>
        {
            context.Response.Redirect("/Auth/Login?error=" + context.Failure?.Message);
            context.HandleResponse();
            return Task.CompletedTask;
        };
    });

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

// Add this temporarily to verify configuration
if (app.Environment.IsDevelopment())
{
    var clientId = builder.Configuration["Authentication:Google:ClientId"];
    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    
    app.Logger.LogInformation("Google ClientId configured: {HasClientId}", !string.IsNullOrEmpty(clientId));
    app.Logger.LogInformation("Google ClientSecret configured: {HasClientSecret}", !string.IsNullOrEmpty(clientSecret));
    
    // Don't log the actual values in production!
    if (!string.IsNullOrEmpty(clientId))
    {
        app.Logger.LogInformation("ClientId starts with: {ClientIdPrefix}", clientId.Substring(0, Math.Min(10, clientId.Length)));
    }
}

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

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SnapSaves.Auth;
using SnapSaves.Data;
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
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

// Configure Cookie Settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Configure MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddTransient<MongoDbSeeder>();

// Add Controllers
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MongoDbSeeder>();
    await seeder.SeedAsync();
}

await app.RunAsync();
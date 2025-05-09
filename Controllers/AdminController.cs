using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admin role
    public class AdminController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ProjectHelper _projectHelper;
        private readonly TemplateHelper _templateHelper;

        public AdminController(AppIdentityDbContext context, RoleManager<IdentityRole> roleManager,
                ProjectHelper _projectHelper, TemplateHelper _templateHelper)
        {
            _context = context;
            _roleManager = roleManager;
            _projectHelper = _projectHelper;
            _templateHelper = _templateHelper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadUniversalTemplate(IFormFile templateZip)
        {
            if (templateZip == null || templateZip.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            // Step 1: Save the uploaded .zip file to a temporary location
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);

            var zipFilePath = Path.Combine(tempPath, templateZip.FileName);
            using (var stream = new FileStream(zipFilePath, FileMode.Create))
            {
                await templateZip.CopyToAsync(stream);
            }

            // Step 2: Extract the .zip file
            var extractPath = Path.Combine(tempPath, "Extracted");
            Directory.CreateDirectory(extractPath);
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            try
            {
                // Step 3: Use ProjectHelper to create a project from the extracted directory
                var adminUserId = "UniversalAdmin"; // Use a special ID for universal templates
                var project = await _projectHelper.CreateProjectFromDirectoryAsync(extractPath, adminUserId);

                var template = await _templateHelper.CreateUniversalTemplateAsync(project); // Use 0 or a special ID for universal templates
                if (template == null)
                {
                    return BadRequest($"Failed to create universal template: ");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Clean up temporary files
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Fetch all roles and their permissions
            var roles = await _roleManager.Roles.ToListAsync();
            var rolePermissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .ToListAsync();

            // Fetch all organizations
            var organizations = await _context.Organizations.ToListAsync();

            // Group permissions by role
            var rolePermissionsViewModel = roles.Select(role => new RolePermissionsViewModel
            {
                RoleName = role.Name,
                Permissions = rolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList()
            }).ToList();

            var universalTemplates = await _templateHelper.GetAllUniversalTemplatesAsync();

            // Create the view model
            var viewModel = new AdminViewModel
            {
                Roles = rolePermissionsViewModel,
                Organizations = organizations,
                UniversalTemplates = universalTemplates
            };

            return View(viewModel);
        }
    }

    public class AdminViewModel
    {
        public List<RolePermissionsViewModel> Roles { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<Template> UniversalTemplates { get; set; }
    }

    public class RolePermissionsViewModel
    {
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}

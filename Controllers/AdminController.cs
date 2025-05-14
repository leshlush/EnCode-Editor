using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly MongoDbContext _mongoDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ProjectHelper _projectHelper;
        private readonly TemplateHelper _templateHelper;

        public AdminController(AppIdentityDbContext context, RoleManager<IdentityRole> roleManager,
                ProjectHelper projectHelper, TemplateHelper templateHelper, MongoDbContext mongoDbContext)
        {
            _context = context;
            _roleManager = roleManager;
            _projectHelper = projectHelper;
            _templateHelper = templateHelper;
            _mongoDbContext = mongoDbContext;
        }

        [HttpPost]
        [RequestSizeLimit(104_857_600)]
        public async Task<IActionResult> UploadUniversalTemplate(
            [FromForm] IFormFile templateZip,
            [FromForm] string projectName,
            [FromForm] string projectDescription,
            [FromForm] bool hasInstructions,
            [FromForm] IFormFile? instructionsZip)
        {
            if (templateZip == null || templateZip.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(projectDescription))
            {
                return BadRequest("Project name and description are required.");
            }

            // Retrieve the current user's MongoUserId from claims
            var adminUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;

            if (string.IsNullOrEmpty(adminUserId))
            {
                return Unauthorized("MongoUserId claim is missing for the current user.");
            }

            string? instructionsId = null;
            if (hasInstructions && instructionsZip != null && instructionsZip.Length > 0)
            {
                // Save the instructions zip to wwwroot/instructions/{guid}/
                var instructionsFolder = Path.Combine("wwwroot", "instructions", Guid.NewGuid().ToString());
                Directory.CreateDirectory(instructionsFolder);

                var instructionsZipFilePath = Path.Combine(instructionsFolder, instructionsZip.FileName);
                using (var stream = new FileStream(instructionsZipFilePath, FileMode.Create))
                {
                    await instructionsZip.CopyToAsync(stream);
                }
                // Extract the zip
                ZipFile.ExtractToDirectory(instructionsZipFilePath, instructionsFolder);

                // Save the relative path to index.html as the Location in Instructions
                var instructions = new Instructions
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = InstructionsType.Static,
                    Location = Path.Combine("instructions", Path.GetFileName(instructionsFolder), "content", "index.html")
                    Description = "Instructions for " + projectName,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Instructions.Add(instructions);
                await _context.SaveChangesAsync();
                instructionsId = instructions.Id;
            }

            // Step 1: Save the uploaded .zip file to a temporary location
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);

            var templateZipFilePath = Path.Combine(tempPath, templateZip.FileName);
            using (var stream = new FileStream(templateZipFilePath, FileMode.Create))
            {
                await templateZip.CopyToAsync(stream);
            }

            // Step 2: Extract the .zip file
            var extractPath = Path.Combine(tempPath, "Extracted");
            Directory.CreateDirectory(extractPath);
            ZipFile.ExtractToDirectory(templateZipFilePath, extractPath);

            try
            {
                // Step 3: Use ProjectHelper to create a project from the extracted directory
                var project = await _projectHelper.CreateProjectFromDirectoryAsync(extractPath, adminUserId);

                // Update the project with the provided name and description
                project.Name = projectName;

                // Create the universal template and set the instructions if present
                var template = await _templateHelper.CreateUniversalTemplateAsync(project, projectDescription);
                if (template == null)
                {
                    return BadRequest("Failed to create universal template.");
                }

                if (!string.IsNullOrEmpty(instructionsId))
                {
                    template.InstructionsId = instructionsId;
                    _context.Templates.Update(template);
                    await _context.SaveChangesAsync();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
                return NotFound();

            // Remove from MySQL
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            // Remove from MongoDB TemplateProjects (if exists)
            if (!string.IsNullOrEmpty(template.MongoId))
            {
                var filter = Builders<Project>.Filter.Eq(p => p.Id, template.MongoId);
                await _mongoDbContext.TemplateProjects.DeleteOneAsync(filter);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProjectFromTemplate(int templateId)
        {
            var template = await _context.Templates.FindAsync(templateId);
            if (template == null)
                return NotFound();

            // Get the current admin's MongoUserId
            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("MongoUserId claim is missing");

            // Use ProjectHelper to create the project
            var result = await _projectHelper.CreateProjectFromTemplateAsync(template, userId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return RedirectToAction("Index", "Projects");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolePermissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .ToListAsync();

            var organizations = await _context.Organizations.ToListAsync();

            var rolePermissionsViewModel = roles.Select(role => new RolePermissionsViewModel
            {
                RoleName = role.Name,
                Permissions = rolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList()
            }).ToList();

            var universalTemplates = await _templateHelper.GetAllUniversalTemplatesAsync();

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

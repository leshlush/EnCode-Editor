using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Helpers;
using SnapSaves.Data;
using SnapSaves.Models.ViewModels;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;


namespace SnapSaves.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly TemplateHelper _templateHelper;
        private readonly AppIdentityDbContext _dbContext;
        private readonly MongoDbContext _mongoDbContext;

        public TemplatesController(TemplateHelper templateHelper, AppIdentityDbContext dbContext, MongoDbContext mongoDbContext)
        {
            _templateHelper = templateHelper;
            _dbContext = dbContext;
            _mongoDbContext = mongoDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplateFromProject(string projectId, int courseId)
        {
            var (success, errorMessage) = await _templateHelper.CreateTemplateFromProjectAsync(projectId, courseId);

            if (!success)
            {
                return BadRequest(errorMessage);
            }

            return RedirectToAction("Details", "Courses", new { id = courseId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DetailsPartial(int id)
        {
            var template = await _dbContext.Templates.FindAsync(id);
            if (template == null)
                return NotFound();

            var userId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Find all TemplateProjects for this template
            var templateProjectMongoIds = await _dbContext.TemplateProjects
                .Where(tp => tp.TemplateId == id)
                .Select(tp => tp.ProjectMongoId)
                .ToListAsync();

            // Find all Projects in MongoDB for this user and these template projects
            var userProjects = await _mongoDbContext.Projects
                .Find(p => p.UserId == userId && templateProjectMongoIds.Contains(p.Id))
                .ToListAsync();

            var viewModel = new TemplateDetailsViewModel
            {
                Template = template,
                UserProjects = userProjects
            };

            return PartialView("_DetailsPartial", viewModel);
        }
    }
}

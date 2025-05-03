using Microsoft.AspNetCore.Mvc;
using SnapSaves.Helpers;

namespace SnapSaves.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly TemplateHelper _templateHelper;

        public TemplatesController(TemplateHelper templateHelper)
        {
            _templateHelper = templateHelper;
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
    }
}

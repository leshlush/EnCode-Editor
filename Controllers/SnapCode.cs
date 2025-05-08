using Microsoft.AspNetCore.Mvc;

namespace SnapSaves.Controllers
{
    public class SnapCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string projectId, string userId)
        {
            // Pass the projectId and userId to the view
            ViewData["ProjectId"] = projectId;
            ViewData["UserId"] = userId;
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SnapSaves.Controllers
{
    public class SnapCode : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

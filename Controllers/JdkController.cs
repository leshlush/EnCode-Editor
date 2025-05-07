using Microsoft.AspNetCore.Mvc;

namespace SnapSaves.Controllers
{
    public class JdkController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }

}

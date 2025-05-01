using Microsoft.AspNetCore.Mvc;

namespace SnapSaves.Controllers
{
    public class CheerpJController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace SnapSaves.Controllers
{
    public class InstructionsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstructionsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Placeholder()
        {
            // Get the absolute path to the placeholder HTML file
            var filePath = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "instructions-placeholder.html");

            // Serve the static placeholder HTML file
            return PhysicalFile(filePath, "text/html");
        }
    }
}

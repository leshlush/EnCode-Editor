using Microsoft.AspNetCore.Mvc;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    public class LessonController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public LessonController(AppIdentityDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int lessonId)
        {
            // Query the database to find the lesson by its ID
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson == null)
            {
                return NotFound("Lesson not found.");
            }

            // Use the Location property from the database
            var lessonPath = $"/{lesson.Location.Replace("\\", "/")}";

            // Check if the file exists in wwwroot
            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", lesson.Location);
            if (!System.IO.File.Exists(physicalPath))
            {
                return NotFound("Lesson file not found.");
            }

            // Pass the lesson path to the view
            ViewData["LessonPath"] = lessonPath;
            return View();
        }
    }
}
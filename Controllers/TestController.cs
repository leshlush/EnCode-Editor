// Controllers/TestController.cs
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        private readonly MongoDbContext _context;

        public TestController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Find(_ => true)
                .ToListAsync();

            return View(projects);
        }
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SnapSaves.Data;

    public class HomeController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public HomeController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userCount = _context.Users.Count();
            ViewData["UserCount"] = userCount;
            return View();
        }
    }

}

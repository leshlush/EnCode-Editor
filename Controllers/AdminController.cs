using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admin role
    public class RolePermissionsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePermissionsController(AppIdentityDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Fetch all roles and their permissions
            var roles = await _roleManager.Roles.ToListAsync();
            var rolePermissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .ToListAsync();

            // Group permissions by role
            var viewModel = roles.Select(role => new RolePermissionsViewModel
            {
                RoleName = role.Name,
                Permissions = rolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList()
            }).ToList();

            return View(viewModel);
        }
    }

    public class RolePermissionsViewModel
    {
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admin role
    public class AdminController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(AppIdentityDbContext context, RoleManager<IdentityRole> roleManager)
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

            // Fetch all organizations
            var organizations = await _context.Organizations.ToListAsync();

            // Group permissions by role
            var rolePermissionsViewModel = roles.Select(role => new RolePermissionsViewModel
            {
                RoleName = role.Name,
                Permissions = rolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList()
            }).ToList();

            // Create the view model
            var viewModel = new AdminViewModel
            {
                Roles = rolePermissionsViewModel,
                Organizations = organizations
            };

            return View(viewModel);
        }
    }

    public class AdminViewModel
    {
        public List<RolePermissionsViewModel> Roles { get; set; }
        public List<Organization> Organizations { get; set; }
    }

    public class RolePermissionsViewModel
    {
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}

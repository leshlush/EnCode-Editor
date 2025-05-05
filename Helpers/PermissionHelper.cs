using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Auth;
using SnapSaves.Data;
using SnapSaves.Models;

public class PermissionHelper
{
    private readonly AppIdentityDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public PermissionHelper(AppIdentityDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> UserHasPermissionAsync(AppUser user, string permissionName)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var roleIds = await _context.Roles
            .Where(r => roles.Contains(r.Name))
            .Select(r => r.Id)
            .ToListAsync();

        return await _context.RolePermissions
            .Include(rp => rp.Permission)
            .AnyAsync(rp => roleIds.Contains(rp.RoleId) && rp.Permission.Name == permissionName);
    }
}

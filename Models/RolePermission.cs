
using Microsoft.AspNetCore.Identity;

namespace SnapSaves.Models
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; } = null!;
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!; // Non-nullable reference type
    }
       
}


namespace SnapSaves.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!; // Non-nullable reference type
    }
       
}

namespace SnapSaves.Models

{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., "viewCourseStudents"
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}

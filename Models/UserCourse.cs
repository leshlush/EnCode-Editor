using SnapSaves.Auth;

namespace SnapSaves.Models
{
    public class UserCourse
    {
        public string UserId { get; set; } = string.Empty; // Initialize with default value
        public AppUser User { get; set; } = null!; // Non-nullable reference type
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!; // Non-nullable reference type
    }
}

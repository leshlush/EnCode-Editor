using SnapSaves.Auth;

namespace SnapSaves.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

}

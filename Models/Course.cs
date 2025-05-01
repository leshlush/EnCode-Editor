using System.Collections.Generic;
using SnapSaves.Auth;

namespace SnapSaves.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Many-to-Many relationship with AppUser
        public ICollection<UserCourse> UserCourses { get; set; }
    }

}

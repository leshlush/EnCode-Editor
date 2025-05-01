// Auth/AppUser.cs
using Microsoft.AspNetCore.Identity;
using SnapSaves.Models;

namespace SnapSaves.Auth
{
    public class AppUser : IdentityUser
    {
        public string MongoUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Role: "Student" or "Teacher"
        public string Role { get; set; }

        // Many-to-Many relationship with Course
        public ICollection<UserCourse> UserCourses { get; set; }
    }


}
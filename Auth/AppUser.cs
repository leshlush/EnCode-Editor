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

        // Foreign key to Organization
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } // Navigation property

        // Many-to-Many relationship with Courses
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}

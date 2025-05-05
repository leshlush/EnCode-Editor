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

        // Keep the Role property for backward compatibility
        public string Role
        {
            get
            {
                // Retrieve the first role assigned to the user
                return Roles?.FirstOrDefault()?.ToString() ?? string.Empty;
            }
        }

        // Navigation property for Identity roles
        public ICollection<IdentityUserRole<string>> Roles { get; set; } = new List<IdentityUserRole<string>>();

        // Many-to-Many relationship with Course
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}

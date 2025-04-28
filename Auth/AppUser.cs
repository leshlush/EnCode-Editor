// Auth/AppUser.cs
using Microsoft.AspNetCore.Identity;

namespace SnapSaves.Auth
{
    public class AppUser : IdentityUser
    {
        // Link to MongoDB User
        public string MongoUserId { get; set; }

        // Additional fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
using SnapSaves.Auth;

namespace SnapSaves.Models
{
    public class Organization
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } = string.Empty; // Organization name
        public string Description { get; set; } = string.Empty; // Organization description
        public string ToolConsumerInstanceGuid { get; set; } = string.Empty; // Unique identifier for the tool consumer
        public OrganizationType Type { get; set; } = OrganizationType.Default; // Organization type
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the organization was created

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>(); // One-to-Many with Courses
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>(); // One-to-Many with AppUsers
    }
}

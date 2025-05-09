using System.ComponentModel.DataAnnotations.Schema;

namespace SnapSaves.Models
{
    public class Course
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } = string.Empty; // Course name
        public string Description { get; set; } = string.Empty; // Course description

        // Foreign key to Organization
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } // Navigation property

        // Many-to-Many relationship with Users
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

        // Many-to-Many relationship with Templates
        public ICollection<CourseTemplate> CourseTemplates { get; set; } = new List<CourseTemplate>();
        
        [NotMapped]
        public List<Template> UniversalTemplates { get; set; }
    }
}

namespace SnapSaves.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Initialize with default value
        public string Description { get; set; } = string.Empty; // Initialize with default value

        // Many-to-Many relationship with AppUser
        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>(); // Initialize with default value

        // Many-to-Many relationship with Template
        public ICollection<CourseTemplate> CourseTemplates { get; set; } = new List<CourseTemplate>(); // Initialize with default value
    }
}

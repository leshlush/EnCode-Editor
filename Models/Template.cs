namespace SnapSaves.Models
{
    public class Template
    {
        public int Id { get; set; } // Primary key in MySQL
        public string MongoId { get; set; } = string.Empty; // MongoDB ID
        public string Name { get; set; } = string.Empty; // Template name
        public string Description { get; set; } = string.Empty; // Template description
        public bool? IsUniversal { get; set; } 

        // Many-to-Many relationship with Course
        public ICollection<CourseTemplate> CourseTemplates { get; set; } = new List<CourseTemplate>();
    }
}

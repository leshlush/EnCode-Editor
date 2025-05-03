namespace SnapSaves.Models
{
    public class CourseTemplate
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!; // Non-nullable reference type
        public int TemplateId { get; set; }
        public Template Template { get; set; } = null!; // Non-nullable reference type
    }
}

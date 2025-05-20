namespace SnapSaves.Models
{
    public class TemplateProject
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string ProjectMongoId { get; set; } = null!;

        public Template Template { get; set; } = null!;
    }

}

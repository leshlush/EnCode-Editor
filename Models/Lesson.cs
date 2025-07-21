using System.ComponentModel.DataAnnotations;

namespace SnapSaves.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty; // URL to the static HTML content

        public string Description { get; set; } = string.Empty;
    }
}

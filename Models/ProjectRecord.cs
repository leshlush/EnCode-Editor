using System;
using System.ComponentModel.DataAnnotations;

namespace SnapSaves.Models
{
    public class ProjectRecord
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string MongoId { get; set; } = string.Empty;

        [Required]
        public string? UserId { get; set; } = string.Empty;

        public int? CourseId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

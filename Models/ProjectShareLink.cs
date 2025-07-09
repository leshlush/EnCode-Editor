using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnapSaves.Models
{
    public class ProjectShareLink
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProjectMongoId { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Token { get; set; } = string.Empty; // Secure random string or GUID

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiresAt { get; set; }

        public bool IsActive { get; set; } = true;

        public string? CreatedByUserId { get; set; } // For auditing (AppUser.Id)
    }
}
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

        // NEW FIELDS FOR ANONYMOUS SUPPORT
        public bool IsAnonymous { get; set; } = false; // Flag for anonymous shares
        public DateTime? LastAccessedAt { get; set; } // Track when anonymous users last accessed
        public string? TemplateId { get; set; } // Track which template this anonymous project came from
        
        // Helper property to check if expired
        [NotMapped]
        public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;
    }
}
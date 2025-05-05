using System.ComponentModel.DataAnnotations;
using SnapSaves.Auth;

namespace SnapSaves.Models
{
    public class LtiUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign key to AppUser.Id

        [Required]
        public string ResourceLinkId { get; set; }

        public string Roles { get; set; }
        public string ContextId { get; set; }
        public string ContextTitle { get; set; }
        public string ContextLabel { get; set; }
        public string ToolConsumerInstanceGuid { get; set; }
        public string ToolConsumerInstanceName { get; set; }

        // Navigation property
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }

}

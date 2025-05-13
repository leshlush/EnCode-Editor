using System.ComponentModel.DataAnnotations.Schema;

namespace SnapSaves.Models
{
    public class Instructions
    {
        public string Id { get; set; } = string.Empty;
        public InstructionsType Type { get; set; }    
        public string Location { get; set; } = string.Empty; 
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public byte[]? Content { get; set; }
        [NotMapped]
        public string? ContentType { get; set; }
        [NotMapped]
        public string? FileName { get; set; }
    }

    public enum InstructionsType
    {
        Static = 0,
        MongoDb = 1
    }
}

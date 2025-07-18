using System.ComponentModel.DataAnnotations.Schema;

namespace SnapSaves.Models
{
    public class LearningItem
    {
        public int Id { get; set; }
        public LearningItemType ItemType { get; set; }
        public string Name { get; set; } = string.Empty; // Name of the Learning Item
        public int? TemplateId { get; set; }
        [NotMapped]
        public Template? Template { get; set; }
        public int Position { get; set; } // Position in the LearningPath

        [NotMapped]
        public LearningPath? LearningPath { get; set; } // Reference to the parent LearningPath
    }

    public enum LearningItemType
    {
        Template,
        Lesson,
        Quiz
    }
}

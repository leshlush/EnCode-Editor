using System.Collections.Generic;

namespace SnapSaves.Models
{
    public class LearningPath
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } = string.Empty; // Learning Path name
        public string Description { get; set; } = string.Empty; // Learning Path description

        // Many-to-Many relationship with LearningItems
        public ICollection<LearningPathItem> LearningPathItems { get; set; } = new List<LearningPathItem>();

        // Units within the LearningPath
        public ICollection<Unit> Units { get; set; } = new List<Unit>();

        // Inject a reference to this LearningPath into all associated LearningItems
        public void InjectSelfIntoLearningItems()
        {
            foreach (var pathItem in LearningPathItems)
            {
                if (pathItem.LearningItem != null)
                {
                    pathItem.LearningItem.LearningPath = this;
                }
            }
        }
    }

    public class Unit
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } = string.Empty; // Unit name
        public int StartPosition { get; set; } // Start of the range
        public int EndPosition { get; set; } // End of the range
        public int LearningPathId { get; set; } // Foreign key to LearningPath
        public LearningPath LearningPath { get; set; } = null!;
    }
}

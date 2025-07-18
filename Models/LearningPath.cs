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
}

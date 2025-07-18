namespace SnapSaves.Models
{
    public class LearningPathItem
    {
        public int LearningPathId { get; set; }
        public LearningPath LearningPath { get; set; } = null!;

        public int LearningItemId { get; set; }
        public LearningItem LearningItem { get; set; } = null!;
    }
}

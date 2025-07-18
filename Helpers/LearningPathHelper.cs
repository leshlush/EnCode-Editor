using Microsoft.EntityFrameworkCore;
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Helpers
{
    public class LearningPathHelper
    {
        private readonly AppIdentityDbContext _context;

        public LearningPathHelper(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<LearningPath>> GetLearningPathsWithItemsAsync()
        {
            // Fetch all learning paths with their associated items and templates
            var learningPaths = await _context.LearningPaths
                .Include(lp => lp.LearningPathItems)
                .ThenInclude(lpi => lpi.LearningItem)
                .ThenInclude(li => li.Template) // Include the Template for hydration
                .ToListAsync();

            // Ensure LearningPathItems and LearningItems are properly loaded
            foreach (var path in learningPaths)
            {
                foreach (var item in path.LearningPathItems)
                {
                    if (item.LearningItem == null)
                    {
                        throw new Exception($"LearningItem is null for LearningPathItem with LearningPathId: {item.LearningPathId} and LearningItemId: {item.LearningItemId}");
                    }
                }
            }

            return learningPaths;
        }
    }
}

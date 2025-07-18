using SnapSaves.Data;
using SnapSaves.Models;
using Microsoft.EntityFrameworkCore;

namespace SnapSaves.Helpers
{
    public class LearningItemHelper
    {
        private readonly AppIdentityDbContext _context;

        public LearningItemHelper(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<LearningItem>> GetLearningItemsWithTemplateNamesAsync()
        {
            var learningItems = await _context.LearningItems
                .Include(li => li.Template) // Include the Template entity
                .ToListAsync();

            // Hydrate the Name property with the Template's Name
            foreach (var item in learningItems)
            {
                if (item.TemplateId.HasValue && item.Template != null)
                {
                    item.Name = item.Template.Name;
                }
            }

            return learningItems;
        }
    }
}
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

        // Create a new LearningPath
        public async Task<LearningPath> CreateLearningPathAsync(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("LearningPath name is required.", nameof(name));
            }

            var learningPath = new LearningPath
            {
                Name = name,
                Description = description ?? string.Empty
            };

            _context.LearningPaths.Add(learningPath);
            await _context.SaveChangesAsync();

            return learningPath;
        }

        // Add LearningItems to a LearningPath
        public async Task AddLearningItemsAsync(int learningPathId, List<LearningItemRequest> learningItems)
        {
            var learningPath = await _context.LearningPaths
                .Include(lp => lp.LearningPathItems)
                .FirstOrDefaultAsync(lp => lp.Id == learningPathId);

            if (learningPath == null)
            {
                throw new KeyNotFoundException("LearningPath not found.");
            }

            foreach (var item in learningItems)
            {
                var learningItem = new LearningItem
                {
                    Name = item.Name,
                    ItemType = item.ItemType,
                    TemplateId = item.TemplateId,
                    LessonId = item.LessonId,
                    Position = item.Position
                };

                _context.LearningItems.Add(learningItem);
                await _context.SaveChangesAsync();

                var learningPathItem = new LearningPathItem
                {
                    LearningPathId = learningPath.Id,
                    LearningItemId = learningItem.Id
                };

                _context.LearningPathItems.Add(learningPathItem);
            }

            await _context.SaveChangesAsync();
        }

        // Add Units to a LearningPath
        public async Task AddUnitsAsync(int learningPathId, List<UnitRequest> units)
        {
            var learningPath = await _context.LearningPaths
                .Include(lp => lp.Units)
                .FirstOrDefaultAsync(lp => lp.Id == learningPathId);

            if (learningPath == null)
            {
                throw new KeyNotFoundException("LearningPath not found.");
            }

            foreach (var unit in units)
            {
                var newUnit = new Unit
                {
                    Name = unit.Name,
                    StartPosition = unit.StartPosition,
                    EndPosition = unit.EndPosition,
                    LearningPathId = learningPath.Id
                };

                _context.Units.Add(newUnit);
            }

            await _context.SaveChangesAsync();
        }

        // Get LearningPaths with associated items for a specific course
        public async Task<List<LearningPath>> GetLearningPathsWithItemsAsync()
        {
            return await _context.LearningPaths
                .Include(lp => lp.LearningPathItems)
                    .ThenInclude(lpi => lpi.LearningItem)
                .Include(lp => lp.Units)
                .Select(lp => new LearningPath
                {
                    Id = lp.Id,
                    Name = lp.Name,
                    Description = lp.Description,
                    Units = lp.Units.OrderBy(u => u.StartPosition).ToList(),
                    LearningPathItems = lp.LearningPathItems.Select(lpi => new LearningPathItem
                    {
                        LearningPathId = lpi.LearningPathId,
                        LearningItemId = lpi.LearningItemId,
                        LearningItem = new LearningItem
                        {
                            Id = lpi.LearningItem.Id,
                            Name = lpi.LearningItem.Name,
                            ItemType = lpi.LearningItem.ItemType,
                            Position = lpi.LearningItem.Position,
                            TemplateId = lpi.LearningItem.TemplateId, // Ensure TemplateId is included
                            LessonId = lpi.LearningItem.LessonId,     // Ensure LessonId is included
                        }
                    }).ToList()
                })
                .ToListAsync();
        }
    }

    // Request models
    public class LearningItemRequest
    {
        public string Name { get; set; } = string.Empty;
        public LearningItemType ItemType { get; set; }
        public int? TemplateId { get; set; }
        public int? LessonId { get; set; }
        public int Position { get; set; }
    }

    public class UnitRequest
    {
        public string Name { get; set; } = string.Empty;
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
    }
}

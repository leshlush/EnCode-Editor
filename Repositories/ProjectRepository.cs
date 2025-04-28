// Repositories/ProjectRepository.cs
using SnapSaves.Data;
using SnapSaves.Models;

namespace SnapSaves.Repositories
{
    public class ProjectRepository
    {
        private readonly MongoDbContext _context;

        public ProjectRepository(MongoDbContext context)
        {
            _context = context;
        }

        // Repository methods here
    }
}
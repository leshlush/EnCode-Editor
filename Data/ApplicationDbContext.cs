using Microsoft.EntityFrameworkCore;
using SnapSaves.Models;

namespace SnapSaves.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LtiUser> LtiUsers { get; set; }
    }
}

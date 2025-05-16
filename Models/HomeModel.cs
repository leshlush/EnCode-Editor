using SnapSaves.Data;

namespace SnapSaves.Models
{
    public class HomeModel
    {
        private readonly AppIdentityDbContext _context;

        public int UserCount { get; set; }

        public HomeModel(AppIdentityDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Simple query: count users
            UserCount = _context.Users.Count();
        }
    }
}

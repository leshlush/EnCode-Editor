using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnapSaves.Auth;
using SnapSaves.Models;

namespace SnapSaves.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        public DbSet<LtiUser> LtiUsers { get; set; } // Keep LtiUsers DbSet
        public DbSet<Course> Courses { get; set; } // Add Courses DbSet
        public DbSet<UserCourse> UserCourses { get; set; } // Add UserCourse DbSet

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure LtiUser relationship
            builder.Entity<LtiUser>()
                .HasOne(l => l.AppUser)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .IsRequired();

            // Configure many-to-many relationship between AppUser and Course
            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId);

            // Configure AppUser Role
            builder.Entity<AppUser>()
                .Property(u => u.Role)
                .IsRequired();
        }
    }
}

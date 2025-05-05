using Microsoft.AspNetCore.Identity;
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
        public DbSet<CourseTemplate> CourseTemplates {  get; set; } // Add CourseTemplates DbSet
        public DbSet<Template> Templates { get; set; } // Add Templates DbSet
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define composite key for CourseTemplate
            builder.Entity<CourseTemplate>()
                .HasKey(ct => new { ct.CourseId, ct.TemplateId });

            // Define relationships for CourseTemplate
            builder.Entity<CourseTemplate>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTemplates)
                .HasForeignKey(ct => ct.CourseId);

            builder.Entity<CourseTemplate>()
                .HasOne(ct => ct.Template)
                .WithMany(t => t.CourseTemplates)
                .HasForeignKey(ct => ct.TemplateId);

            // Define composite key for UserCourse
            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            // Configure relationships for IdentityUserRole
            builder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<IdentityUserRole<string>>()
                .HasOne<IdentityRole>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<IdentityUserRole<string>>()
                .HasOne<AppUser>()
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);
        }



    }
}

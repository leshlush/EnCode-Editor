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

        public DbSet<LtiUser> LtiUsers { get; set; } 
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; } 
        public DbSet<CourseTemplate> CourseTemplates {  get; set; } 
        public DbSet<Template> Templates { get; set; } 
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Template
            builder.Entity<Template>(entity =>
            {
                entity.Property(t => t.MongoId).IsRequired().HasMaxLength(255);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description).IsRequired();
            });

            // Configure CourseTemplate
            builder.Entity<CourseTemplate>(entity =>
            {
                entity.HasKey(ct => new { ct.CourseId, ct.TemplateId });

                entity.HasOne(ct => ct.Course)
                    .WithMany(c => c.CourseTemplates)
                    .HasForeignKey(ct => ct.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ct => ct.Template)
                    .WithMany(t => t.CourseTemplates)
                    .HasForeignKey(ct => ct.TemplateId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Organization relationships
            builder.Entity<Organization>()
                .HasMany(o => o.Courses)
                .WithOne(c => c.Organization)
                .HasForeignKey(c => c.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Organization>()
                .HasMany(o => o.Users)
                .WithOne(u => u.Organization)
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Course relationships
            builder.Entity<Course>()
                .HasOne(c => c.Organization)
                .WithMany(o => o.Courses)
                .HasForeignKey(c => c.OrganizationId);

            // Configure AppUser relationships
            builder.Entity<AppUser>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OrganizationId);

            // Configure RolePermission relationships
            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany()
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // Configure CourseTemplate relationships
            builder.Entity<CourseTemplate>()
                .HasKey(ct => new { ct.CourseId, ct.TemplateId });

            builder.Entity<CourseTemplate>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTemplates)
                .HasForeignKey(ct => ct.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CourseTemplate>()
                .HasOne(ct => ct.Template)
                .WithMany(t => t.CourseTemplates)
                .HasForeignKey(ct => ct.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure UserCourse relationships
            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure LtiUser relationships
            builder.Entity<LtiUser>()
                .HasOne(lu => lu.AppUser)
                .WithMany()
                .HasForeignKey(lu => lu.AppUserId)
                .IsRequired(false);

            // Additional configurations for other entities can go here...
        }





    }
}

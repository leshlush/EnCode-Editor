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
        public DbSet<CourseTemplate> CourseTemplates { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Instructions> Instructions { get; set; }
        public DbSet<TemplateProject> TemplateProjects { get; set; }
        public DbSet<ProjectRecord> ProjectRecords { get; set; }
        public DbSet<ProjectShareLink> ProjectShareLinks { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LearningItem> LearningItems { get; set; }
        public DbSet<LearningPathItem> LearningPathItems { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TemplateProject: Each TemplateProject references one Template (many-to-one)
            builder.Entity<TemplateProject>(entity =>
            {
                entity.ToTable("templateprojects");
                entity.HasKey(tp => tp.Id);

                entity.Property(tp => tp.ProjectMongoId)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.HasOne(tp => tp.Template)
                    .WithMany() // If you add a collection to Template: .WithMany(t => t.TemplateProjects)
                    .HasForeignKey(tp => tp.TemplateId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Force Identity tables to lowercase
            builder.Entity<AppUser>().ToTable("aspnetusers");
            builder.Entity<IdentityRole>().ToTable("aspnetroles");
            builder.Entity<IdentityUserRole<string>>().ToTable("aspnetuserroles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("aspnetuserclaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("aspnetuserlogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("aspnetusertokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("aspnetroleclaims");

            builder.Entity<LtiUser>().ToTable("ltiusers");
            builder.Entity<Course>().ToTable("courses");
            builder.Entity<UserCourse>().ToTable("usercourses");
            builder.Entity<CourseTemplate>().ToTable("coursetemplates");
            builder.Entity<Template>().ToTable("templates");
            builder.Entity<Permission>().ToTable("permissions");
            builder.Entity<Organization>().ToTable("organizations");
            builder.Entity<RolePermission>().ToTable("rolepermissions");
            builder.Entity<Instructions>().ToTable("instructions");
            builder.Entity<ProjectRecord>().ToTable("projectrecords");

            builder.Entity<ProjectShareLink>().ToTable("projectsharelinks");
            builder.Entity<TemplateProject>().ToTable("templateprojects");
            builder.Entity<LearningPath>().ToTable("learningpaths");
            builder.Entity<LearningItem>().ToTable("learningitems");
            builder.Entity<LearningPathItem>().ToTable("learningpathitems");
            builder.Entity<Unit>().ToTable("units"); // Configure Unit table
            builder.Entity<Lesson>().ToTable("lessons");


            builder.Entity<ProjectShareLink>(entity =>
            {
                entity.ToTable("projectsharelinks");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.ProjectMongoId).IsRequired().HasMaxLength(255);
                entity.Property(l => l.Token).IsRequired().HasMaxLength(128);
                entity.Property(l => l.IsActive).HasDefaultValue(true);
            });

            // Configure Template
            builder.Entity<Template>(entity =>
            {
                entity.Property(t => t.MongoId).IsRequired().HasMaxLength(255);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description).IsRequired();
                entity.Property(t => t.AllowAnonymousAccess).HasDefaultValue(false);
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
            builder.Entity<Organization>(entity =>
            {
                entity.Property(o => o.Type)
                    .HasDefaultValue(OrganizationType.Default)
                    .HasConversion<int>(); // Store enum as int in database
                
                entity.Property(o => o.CreatedAt)
                    .HasDefaultValue(DateTime.UtcNow);

                entity.HasMany(o => o.Courses)
                    .WithOne(c => c.Organization)
                    .HasForeignKey(c => c.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Users)
                    .WithOne(u => u.Organization)
                    .HasForeignKey(u => u.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

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
            
            // Configure LearningItem
            builder.Entity<LearningItem>(entity =>
            {
                entity.ToTable("learningitems");
                entity.HasKey(li => li.Id);

                entity.Property(li => li.Name)
                    .IsRequired();

                entity.Property(li => li.ItemType)
                    .IsRequired();

                entity.Property(li => li.Position)
                    .IsRequired()
                    .HasDefaultValue(0);

                // Configure TemplateId as a foreign key without navigation
                entity.Property(li => li.TemplateId);

                // Configure LessonId as a foreign key without navigation
                entity.Property(li => li.LessonId);
            });

            // Configure Lesson
            builder.Entity<Lesson>(entity =>
            {
                entity.ToTable("lessons");
                entity.HasKey(l => l.Id);

                entity.Property(l => l.Location)
                    .IsRequired();

                entity.Property(l => l.Description)
                    .IsRequired();
            });

            // Configure LearningPathItem
            builder.Entity<LearningPathItem>(entity =>
            {
                entity.ToTable("learningpathitems");
                entity.HasKey(lpi => new { lpi.LearningPathId, lpi.LearningItemId });

                entity.HasOne(lpi => lpi.LearningPath)
                    .WithMany(lp => lp.LearningPathItems)
                    .HasForeignKey(lpi => lpi.LearningPathId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(lpi => lpi.LearningItem)
                    .WithMany()
                    .HasForeignKey(lpi => lpi.LearningItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure LearningPath
            builder.Entity<LearningPath>(entity =>
            {
                entity.ToTable("learningpaths");
                entity.HasKey(lp => lp.Id);

                entity.Property(lp => lp.Name)
                    .IsRequired();

                entity.Property(lp => lp.Description)
                    .IsRequired();

                // Configure relationship with LearningPathItems
                entity.HasMany(lp => lp.LearningPathItems)
                    .WithOne(lpi => lpi.LearningPath)
                    .HasForeignKey(lpi => lpi.LearningPathId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure relationship with Units
                entity.HasMany(lp => lp.Units)
                    .WithOne(u => u.LearningPath)
                    .HasForeignKey(u => u.LearningPathId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Unit
            builder.Entity<Unit>(entity =>
            {
                entity.ToTable("units");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired();

                entity.Property(u => u.StartPosition)
                    .IsRequired();

                entity.Property(u => u.EndPosition)
                    .IsRequired();

                entity.HasOne(u => u.LearningPath)
                    .WithMany(lp => lp.Units)
                    .HasForeignKey(u => u.LearningPathId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Other configurations (e.g., Identity, Courses, etc.) remain unchanged
        }
    }
}

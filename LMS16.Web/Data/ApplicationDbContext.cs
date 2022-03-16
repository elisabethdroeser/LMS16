using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS16.Core.Entities;

#nullable disable
namespace LMS16.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LMS16.Core.Entities.User> User { get; set; }
        public DbSet<LMS16.Core.Entities.Activity> Activity { get; set; }
        public DbSet<LMS16.Core.Entities.ActivityType> ActivityType { get; set; }
        public DbSet<LMS16.Core.Entities.Course> Course { get; set; }
        public DbSet<LMS16.Core.Entities.Module> Module { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Activity>().HasKey(a => new { a.ModuleId, a.ActivityTypeId });

            modelBuilder.Entity<Module>().HasKey(m => new { m.CourseId });

            modelBuilder.Entity<User>().HasKey(u => new { u.CourseId });
        }
    }
}
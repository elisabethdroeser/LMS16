using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS16.Core.Entities;
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace LMS16.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<LMS16.Core.Entities.Activity> Activity { get; set; }
        public DbSet<LMS16.Core.Entities.ActivityType> ActivityType { get; set; }
        public DbSet<LMS16.Core.Entities.Course> Course { get; set; }
        public DbSet<LMS16.Core.Entities.Module> Module { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
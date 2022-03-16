using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS16.Core.Entities;

#nullable disable
namespace LMS16.Data
{
    public class ApplicationDbContext : DbContext
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
    }
}
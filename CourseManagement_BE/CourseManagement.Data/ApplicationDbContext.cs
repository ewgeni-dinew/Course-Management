namespace CourseManagement.Data
{
    using CourseManagement.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<FavoriteCourse> FavoriteCourses { get; set; }

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
        }
    }
}

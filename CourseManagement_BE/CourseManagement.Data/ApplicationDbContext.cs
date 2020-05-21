namespace CourseManagement.Data
{
    using CourseManagement.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<FavoriteCourse> FavoriteCourses { get; set; }

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Favorites)
                .WithOne(f => f.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Course>()
                .HasMany(x => x.Favorites)
                .WithOne(f => f.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                            new Role { Name = "User", Id = 1 },
                            new Role { Name = "Admin", Id = 2 }
                            );

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = 1, Username = "admin@test.com", FirstName = "Admin", LastName = "Adminov", Password = "1234", RoleId = 2 });
        }
    }
}

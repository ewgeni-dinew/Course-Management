namespace CourseManagement.Data
{
    using CourseManagement.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<FavoriteCourse> FavoriteCourses { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public ApplicationDbContext() { } //for unit testing purposes only

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

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
                            new Role(1, "User"),
                            new Role(2, "Admin")
                            );

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser(1, "admin@test.com", "Sb123456", "Admin", "Adminov", 2));
        }
    }
}

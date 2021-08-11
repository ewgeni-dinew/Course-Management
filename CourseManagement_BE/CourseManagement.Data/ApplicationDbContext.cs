namespace CourseManagement.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using CourseManagement.Data.Models;
    using CourseManagement.Data.Models.Contracts;

    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<FavoriteCourse> FavoriteCourses { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public ApplicationDbContext() { } //for UNIT tests

        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public override int SaveChanges()
        {
            UpdateEntityDatesOnChange();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntityDatesOnChange();

            return base.SaveChangesAsync(cancellationToken);
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
                            new Role(1, "User"),
                            new Role(2, "Admin"));

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser(1, "admin@test.com", "Sb123456", "Admin", "Adminov", 2));
        }

        private void UpdateEntityDatesOnChange()
        {
            var entries = ChangeTracker.Entries()
                                                .Where(x => x.Entity is IDatable &&
                                                (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IDatable)entry.Entity).CreatedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((IDatable)entry.Entity).UpdatedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

namespace CourseManagement.Data
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        [System.Obsolete]
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = "Server=DESKTOP-B6V15QN\\SQLEXPRESS;Database=CourseManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            builder.UseSqlServer(connectionString);

            // Stop client query evaluation
            builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            var dbContext = new ApplicationDbContext(builder.Options);

            return dbContext;
        }
    }
}

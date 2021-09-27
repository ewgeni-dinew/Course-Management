namespace CourseManagement.Data
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Helper class to create and use Database Migrations
    /// Gets the Connection string from the AppSettings.json file
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string CONNECTION_STRING_KEY = "DefaultConnection";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration.GetConnectionString(CONNECTION_STRING_KEY);

            builder.UseSqlServer(connectionString);

            // Stop client query evaluation
            //builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            var dbContext = new ApplicationDbContext(builder.Options);

            return dbContext;
        }
    }
}

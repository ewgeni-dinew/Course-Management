namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Repository.Contracts;
    using System.Threading.Tasks;

    public abstract class Repository : ISaveble
    {
        internal ApplicationDbContext DbContext { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int Save()
        {
            return this.DbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
    }
}

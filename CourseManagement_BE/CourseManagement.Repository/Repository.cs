namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models.Contracts;
    using CourseManagement.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Repository<T> : IRepository<T>
        where T : class, IIdentifiable
    {
        internal ApplicationDbContext DbContext { get; }

        public IQueryable<T> GetAll
        {
            get
            {
                return this.DbContext.Set<T>().AsQueryable();
            }
        }

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

        public async Task<T> GetById(int id)
        {
            return await this.DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Delete(T entity)
        {
            this.DbContext.Set<T>().Remove(entity);
        }

        public void Create(T entity)
        {
            this.DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.DbContext.Update(entity);
        }
    }
}

namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : IRepository<ApplicationUser>, ISaveble
    {
        internal ApplicationDbContext DbContext { get; }

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void Create(ApplicationUser entity)
        {
            this.DbContext.Users.Add(entity);
        }

        public void Update(ApplicationUser entity)
        {
            this.DbContext.Update(entity);
        }

        public void Delete(ApplicationUser entity)
        {
            this.DbContext.Users.Remove(entity);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.DbContext.Users;
        }

        public async Task<ApplicationUser> GetById(int id)
        {
            return await this.DbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
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

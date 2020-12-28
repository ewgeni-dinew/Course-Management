namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : Repository, IRepository<ApplicationUser>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

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
            return await Task.Run(() => this.DbContext.Users.FirstOrDefault(x => x.Id.Equals(id)));
        }
    }
}

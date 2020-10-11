namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class FavoriteCourseRepository : Repository, IRepository<FavoriteCourse>
    {
        public FavoriteCourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

        public void Create(FavoriteCourse entity)
        {
            this.DbContext.FavoriteCourses.Add(entity);
        }

        public void Update(FavoriteCourse entity)
        {
            this.DbContext.Update(entity);
        }

        public void Delete(FavoriteCourse entity)
        {
            this.DbContext.FavoriteCourses.Remove(entity);
        }

        public IQueryable<FavoriteCourse> GetAll()
        {
            return this.DbContext.FavoriteCourses;
        }

        public async Task<FavoriteCourse> GetById(int id)
        {
            return await this.DbContext.FavoriteCourses.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}

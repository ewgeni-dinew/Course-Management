namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseRepository : Repository, IRepository<Course>
    {
        public CourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

        public void Create(Course entity)
        {
            this.DbContext.Courses.Add(entity);
        }

        public void Update(Course entity)
        {
            this.DbContext.Update(entity);
        }

        public void Delete(Course entity)
        {
            this.DbContext.Courses.Remove(entity);
        }

        public IQueryable<Course> GetAll()
        {
            return this.DbContext.Courses;
        }

        public async Task<Course> GetById(int id)
        {
            return await this.DbContext.Courses.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}

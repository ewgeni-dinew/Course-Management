namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;

    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}

namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;

    public class UserCourseRepository : Repository<UserCourse>
    {
        public UserCourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}

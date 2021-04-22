namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;

    public class FavoriteCourseRepository : Repository<FavoriteCourse>
    {
        public FavoriteCourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}

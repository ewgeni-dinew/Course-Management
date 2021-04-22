namespace CourseManagement.Repository
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;

    public class UserRepository : Repository<ApplicationUser>
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}

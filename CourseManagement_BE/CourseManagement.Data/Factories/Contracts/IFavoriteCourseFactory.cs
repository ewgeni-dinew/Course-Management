namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;

    public interface IFavoriteCourseFactory : IFactory<FavoriteCourse>
    {
        IFavoriteCourseFactory WithCourseId(int courseId);

        IFavoriteCourseFactory WithUserId(int userId);
    }
}

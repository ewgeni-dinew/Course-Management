namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;

    public interface IFavoriteCourseFactory : IFactory<FavoriteCourse>
    {
        IFavoriteCourseFactory WithId(int id);

        IFavoriteCourseFactory WithCourseId(int courseId);

        IFavoriteCourseFactory WithUserId(int userId);
    }
}

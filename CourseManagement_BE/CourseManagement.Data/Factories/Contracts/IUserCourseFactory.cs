namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;
    using CourseManagement.Data.Models.Enums;

    public interface IUserCourseFactory : IFactory<UserCourse>
    {
        public IUserCourseFactory WithId(int id);

        public IUserCourseFactory WithUserId(int userId);

        public IUserCourseFactory WithCourseId(int courseId);

        public IUserCourseFactory WithState(UserCourseState state);
    }
}

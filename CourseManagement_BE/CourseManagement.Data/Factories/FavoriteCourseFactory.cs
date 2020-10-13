namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class FavoriteCourseFactory : IFavoriteCourseFactory
    {
        private int courseId;
        private int userId;

        public IFavoriteCourseFactory WithCourseId(int courseId)
        {
            this.courseId = courseId;

            return this;
        }

        public IFavoriteCourseFactory WithUserId(int userId)
        {
            this.userId = userId;

            return this;
        }

        public FavoriteCourse Build()
        {
            return new FavoriteCourse(this.userId, this.courseId);
        }
    }
}

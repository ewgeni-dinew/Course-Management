namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class FavoriteCourseFactory : IFavoriteCourseFactory
    {
        private int id;
        private int courseId;
        private int userId;

        public IFavoriteCourseFactory WithId(int id)
        {
            this.id = id;

            return this;
        }

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
            if (!this.id.Equals(0))
            {
                return new FavoriteCourse(this.userId, this.courseId);
            }

            return new FavoriteCourse(this.id, this.userId, this.courseId);
        }
    }
}

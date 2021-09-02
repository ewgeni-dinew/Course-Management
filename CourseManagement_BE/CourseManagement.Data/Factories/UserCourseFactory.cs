namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.Data.Models.Enums;

    public class UserCourseFactory : IUserCourseFactory
    {
        private int id;
        private int userId;
        private int courseId;
        private UserCourseState state;

        public IUserCourseFactory WithId(int id)
        {
            this.id = id;

            return this;
        }

        public IUserCourseFactory WithUserId(int userId)
        {
            this.userId = userId;

            return this;
        }

        public IUserCourseFactory WithCourseId(int courseId)
        {
            this.courseId = courseId;

            return this;
        }

        public IUserCourseFactory WithState(UserCourseState state)
        {
            this.state = state;

            return this;
        }

        public UserCourse Build()
        {
            if (!this.id.Equals(0))
            {
                return new UserCourse(this.userId, this.courseId, this.state);
            }

            return new UserCourse(this.id, this.userId, this.courseId, this.state);
        }
    }
}

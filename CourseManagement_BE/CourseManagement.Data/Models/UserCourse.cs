namespace CourseManagement.Data.Models
{
    using CourseManagement.Data.Models.Contracts;
    using CourseManagement.Data.Models.Enums;

    public class UserCourse : IIdentifiable
    {
        internal UserCourse(int userId, int courseId, UserCourseState state)
        {
            this.UserId = userId;

            this.CourseId = courseId;

            this.State = state;
        }

        internal UserCourse(int id, int userId, int courseId, UserCourseState state)
        : this(userId, courseId, state)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public int UserId { get; private set; }

        public virtual ApplicationUser User { get; private set; }

        public int CourseId { get; private set; }

        public virtual Course Course { get; private set; }

        public UserCourseState State { get; private set; }

        //
        //METHODS
        //

        public void UpdateCourseState(UserCourseState state)
        {
            this.State = state;
        }
    }
}

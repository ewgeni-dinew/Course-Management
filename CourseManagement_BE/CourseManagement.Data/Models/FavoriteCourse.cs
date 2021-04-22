namespace CourseManagement.Data.Models
{
    using CourseManagement.Data.Models.Contracts;

    public class FavoriteCourse : IIdentifiable
    {
        internal FavoriteCourse(int userId, int courseId)
        {
            this.CourseId = courseId;
            this.UserId = userId;
        }

        internal FavoriteCourse(int id, int userId, int courseId)
        : this(userId, courseId)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public virtual ApplicationUser User { get; private set; }

        public int UserId { get; private set; }

        public virtual Course Course { get; private set; }

        public int CourseId { get; private set; }
    }
}

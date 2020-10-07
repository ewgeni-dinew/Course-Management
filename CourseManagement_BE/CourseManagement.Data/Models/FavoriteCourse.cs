namespace CourseManagement.Data.Models
{
    public class FavoriteCourse
    {
        internal FavoriteCourse()
        {

        }

        public int Id { get; private set; }

        public ApplicationUser User { get; private set; }

        public int UserId { get; private set; }

        public Course Course { get; private set; }

        public int CourseId { get; private set; }
    }
}

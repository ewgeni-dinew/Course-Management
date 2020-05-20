namespace CourseManagement.Data.Models
{
    public class FavoriteCourse
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public int UserId { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }
    }
}

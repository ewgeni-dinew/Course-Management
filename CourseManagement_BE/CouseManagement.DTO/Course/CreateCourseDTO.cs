namespace CouseManagement.DTO.Course
{
    public class CreateCourseDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Summary { get; set; }

        public int AuthorId { get; set; }
    }
}

namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class CreateCourseDTO
    {
        [NotNullOrEmpty]
        public string Title { get; set; }

        [NotNullOrEmpty]
        public string Content { get; set; }

        [NotNullOrEmpty]
        public string Summary { get; set; }

        [NotNullOrEmpty]
        [NumberRange(1)]
        public int AuthorId { get; set; }
    }
}

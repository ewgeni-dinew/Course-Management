namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class EditCourseDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int Id { get; set; }

        [NotNullOrEmpty]
        public string Title { get; set; }

        [NotNullOrEmpty]
        public string Summary { get; set; }

        [NotNullOrEmpty]
        public string Content { get; set; }
    }
}

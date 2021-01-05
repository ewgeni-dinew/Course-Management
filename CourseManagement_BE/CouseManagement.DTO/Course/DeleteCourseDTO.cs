namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class DeleteCourseDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int Id { get; set; }
    }
}

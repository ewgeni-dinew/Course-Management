namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class RateCourseDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int CourseId { get; set; }

        [NotNullOrEmpty]
        [NumberRange(1, 10)]
        public short Rating { get; set; }
    }
}

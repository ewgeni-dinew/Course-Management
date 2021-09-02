namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class ChangeCourseStateDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int UserId { get; set; }

        [NotNullOrEmpty]
        [NumberRange(1)]
        public int CourseId { get; set; }

        [NotNullOrEmpty]
        [NumberRange(1)]
        public int UserCourseState { get; set; }
    }
}

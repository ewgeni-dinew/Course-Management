namespace CourseManagement.DTO.Course
{
    using CourseManagement.DTO.Attributes;

    public class AddToFavoritesDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int CourseId { get; set; }
    }
}

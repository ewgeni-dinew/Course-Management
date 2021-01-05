namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class BaseUserDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int Id { get; set; }
    }
}

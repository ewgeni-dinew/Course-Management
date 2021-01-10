namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class ChangePasswordDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int Id { get; set; }

        [NotNullOrEmpty]
        [Password]
        public string Password { get; set; }

        [NotNullOrEmpty]
        [Password]
        public string NewPassword { get; set; }
    }
}

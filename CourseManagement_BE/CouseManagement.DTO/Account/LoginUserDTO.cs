namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class LoginUserDTO
    {
        [NotNullOrEmpty]
        [Email]
        public string Username { get; set; }

        [NotNullOrEmpty]
        [Password]
        public string Password { get; set; }
    }
}

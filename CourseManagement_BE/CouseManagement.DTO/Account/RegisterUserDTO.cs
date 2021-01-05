namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class RegisterUserDTO
    {
        [NotNullOrEmpty]
        [Email]
        public string Username { get; set; }

        [NotNullOrEmpty]
        //[Password] not ready to test yet
        public string Password { get; set; }

        [NotNullOrEmpty]
        public string FirstName { get; set; }

        [NotNullOrEmpty]
        public string LastName { get; set; }
    }
}

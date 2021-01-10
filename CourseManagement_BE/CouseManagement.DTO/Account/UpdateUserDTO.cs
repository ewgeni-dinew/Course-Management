namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class UpdateUserDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int Id { get; set; }

        [NotNullOrEmpty]
        public string FirstName { get; set; }

        [NotNullOrEmpty]
        public string LastName { get; set; }
    }
}

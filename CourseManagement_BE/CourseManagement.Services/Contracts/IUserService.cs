namespace CourseManagement.Services.Contracts
{
    using CouseManagement.DTO.Account;
    using System.Collections.Generic;

    public interface IUserService
    {
        public UserDetailsDTO LoginUser(LoginUserDTO dto);

        public void RegisterUser(RegisterUserDTO dto);

        public UserDetailsDTO UpdateUser(UpdateUserDTO dto);

        public void DeleteUser(BaseUserDTO dto);

        public ICollection<UserDetailsDTO> GetAllUsers(UpdateUserDTO dto);
        
        public void BlockUser(BaseUserDTO dto);
        
        public void UnblockUser(BaseUserDTO dto);
    }
}

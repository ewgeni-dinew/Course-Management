namespace CourseManagement.Services.Contracts
{
    using CouseManagement.DTO.Account;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<UserDetailsDTO> LoginUser(LoginUserDTO dto);

        public void RegisterUser(RegisterUserDTO dto);

        public UserDetailsDTO UpdateUser(UpdateUserDTO dto);

        public Task DeleteUser(BaseUserDTO dto);

        public Task<ICollection<UserDetailsDTO>> GetAllUsers();
        
        public void BlockUser(BaseUserDTO dto);
        
        public void UnblockUser(BaseUserDTO dto);
    }
}

namespace CourseManagement.Services.Contracts
{
    using CouseManagement.DTO.Account;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<UserDetailsDTO> LoginUser(LoginUserDTO dto);

        public Task RegisterUser(RegisterUserDTO dto);

        public Task<UserDetailsDTO> UpdateUser(UpdateUserDTO dto);

        public Task DeleteUser(BaseUserDTO dto);

        public Task<ICollection<UserDetailsDTO>> GetAllUsers();
        
        public Task BlockUser(BaseUserDTO dto);
        
        public Task UnblockUser(BaseUserDTO dto);
    }
}

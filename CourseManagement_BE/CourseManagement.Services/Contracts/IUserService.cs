namespace CourseManagement.Services.Contracts
{
    using CourseManagement.DTO.Account;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<UserDetailsDTO> LoginUser(LoginUserDTO dto);

        public Task<UserDetailsDTO> RegisterUser(RegisterUserDTO dto);

        public Task<UserDetailsDTO> UpdateUser(UpdateUserDTO dto);

        public Task<int> DeleteUser(BaseUserDTO dto);

        public Task<ICollection<UserDetailsDTO>> GetAllUsers();
        
        public Task<UserDetailsDTO> BlockUser(BaseUserDTO dto);
        
        public Task<UserDetailsDTO> UnblockUser(BaseUserDTO dto);
    }
}

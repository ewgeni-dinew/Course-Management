namespace CourseManagement.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CourseManagement.DTO.Account;

    public interface IUserService
    {
        public Task<UserDetailsDTO> LoginUser(LoginUserDTO dto);

        public Task<UserDetailsDTO> RegisterUser(RegisterUserDTO dto);

        public Task<UserDetailsDTO> UpdateUser(UpdateUserDTO dto);

        public Task<int> DeleteUser(BaseUserDTO dto);

        public Task<ICollection<UserDetailsDTO>> GetAllUsers();

        public Task<UserDetailsDTO> BlockUser(BaseUserDTO dto);

        public Task<UserDetailsDTO> UnblockUser(BaseUserDTO dto);

        public Task<int> ChangePassword(ChangePasswordDTO dto);

        public Task<Tuple<string, string>> RefreshToken(int userId, string refreshToken);

        public Task<string> RevokeToken(int userId, string refreshToken);
    }
}

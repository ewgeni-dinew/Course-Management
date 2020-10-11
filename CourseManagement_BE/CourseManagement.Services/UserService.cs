namespace CourseManagement.Services
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using CouseManagement.DTO.Account;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly IUserFactory userFactory;
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository, IUserFactory userFactory)
        {
            this.userFactory = userFactory;
            this.userRepository = userRepository;
        }

        public UserDetailsDTO LoginUser(LoginUserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterUser(RegisterUserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public UserDetailsDTO UpdateUser(UpdateUserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(BaseUserDTO dto)
        {
            throw new System.NotImplementedException();
        }
                
        public ICollection<UserDetailsDTO> GetAllUsers(UpdateUserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void BlockUser(BaseUserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void UnblockUser(BaseUserDTO dto)
        {
            throw new System.NotImplementedException();
        }       
    }
}

namespace CourseManagement.Services
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using CouseManagement.DTO.Account;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserFactory userFactory;
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository, IUserFactory userFactory)
        {
            this.userFactory = userFactory;
            this.userRepository = userRepository;
        }

        public async Task<UserDetailsDTO> LoginUser(LoginUserDTO dto)
        {
            var user = await this.userRepository.GetAll()?
                .Include(x => x.Role)
                .Where(x => !x.IsBlocked)
                .FirstOrDefaultAsync(x => x.Username.Equals(dto.Username) && x.Password.Equals(dto.Password));

            if (user == null)
            {
                throw new ArgumentException("Invalid User!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("1p4kdl13pr0w[pkda;;kado[po");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.UpdateToken(tokenHandler.WriteToken(token));

            await userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Token = user.Token,
                Role = user.Role.Name,
            };

            return result;
        }

        public async Task RegisterUser(RegisterUserDTO dto)
        {
            if (this.userRepository.GetAll().Any(x => x.Username.Equals(dto.Username)))
            {
                //throw error;
            }

            var user = this.userFactory
                .WithFirstName(dto.FirstName)
                .WithLastName(dto.LastName)
                .WithUsername(dto.Username)
                .WithPassword(dto.Password)
                .WithRoleId(1) //should probably be changed
                .Build();

            this.userRepository.Create(user);

            await this.userRepository.SaveAsync();
        }

        public async Task<UserDetailsDTO> UpdateUser(UpdateUserDTO dto)
        {
            var user = await this.userRepository.GetById(dto.Id);

            if (user == null)
            {
                //throw expection;
            }

            if (dto.Password != "")
            {
                user.UpdatePassword(dto.Password);
            }

            user.UpdateFirstName(dto.FirstName);
            user.UpdateLastName(dto.LastName);

            userRepository.Update(user);

            await this.userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
            };

            return result;
        }

        public async Task DeleteUser(BaseUserDTO dto)
        {
            var user = await this.userRepository.GetById(dto.Id);

            if (user == null)
            {
                //throw exception;
            }

            this.userRepository.Delete(user);

            await this.userRepository.SaveAsync();
        }

        public async Task<ICollection<UserDetailsDTO>> GetAllUsers()
        {
            var users = await this.userRepository.GetAll()
                .Include(x => x.Role)
                .Where(x => x.Role.Name.Equals("User"))
                .Select(x => new UserDetailsDTO
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    IsBlocked = x.IsBlocked
                })
                .ToListAsync();

            return users;
        }

        public async Task BlockUser(BaseUserDTO dto)
        {
            var user = await this.userRepository.GetById(dto.Id);

            if (user == null)
            {
                //throw exception;
            }

            user.Block();

            userRepository.Update(user);

            await this.userRepository.SaveAsync();
        }

        public async Task UnblockUser(BaseUserDTO dto)
        {
            var user = await this.userRepository.GetById(dto.Id);

            if (user == null)
            {
                //throw exception;
            }

            user.Unblock();

            userRepository.Update(user);

            await this.userRepository.SaveAsync();
        }
    }
}

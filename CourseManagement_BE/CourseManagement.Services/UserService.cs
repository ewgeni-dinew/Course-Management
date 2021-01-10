namespace CourseManagement.Services
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using CourseManagement.DTO.Account;
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using CourseManagement.Utilities.Errors;
    using CourseManagement.Utilities.Constants;

    /// <summary>
    /// The class is part of the application Service layer. It handles all the Business Logic connected to the ApplicationUser logical space.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserFactory _userFactory;
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserService(IRepository<ApplicationUser> userRepository, IUserFactory userFactory)
        {
            this._userFactory = userFactory;
            this._userRepository = userRepository;
        }

        public async Task<UserDetailsDTO> LoginUser(LoginUserDTO dto)
        {
            var user = await Task.Run(() => this._userRepository.GetAll()?
                .Include(x => x.Role)
                .Where(x => !x.IsBlocked)
                .FirstOrDefault(x => x.Username.Equals(dto.Username) && x.Password.Equals(dto.Password))
            );

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_USER_CREDENTIALS);
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

            await _userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = user.Token,
                Role = user.Role.Name,
            };

            return result;
        }

        public async Task<UserDetailsDTO> RegisterUser(RegisterUserDTO dto)
        {
            if (this._userRepository.GetAll().AsNoTracking().Any(x => x.Username.Equals(dto.Username)))
            {
                //username is already taken
                throw new CustomException(ErrorMessages.INVALID_USERNAME);
            }

            var user = this._userFactory
                .WithFirstName(dto.FirstName)
                .WithLastName(dto.LastName)
                .WithUsername(dto.Username)
                .WithPassword(dto.Password)
                .WithRoleId(Constants.USER_ROLE_ID)
                .Build();

            this._userRepository.Create(user);

            await this._userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return result;
        }

        public async Task<UserDetailsDTO> UpdateUser(UpdateUserDTO dto)
        {
            var user = await this._userRepository.GetById(dto.Id);

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.UpdateFirstName(dto.FirstName);
            user.UpdateLastName(dto.LastName);

            _userRepository.Update(user);

            await this._userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return result;
        }

        public async Task<int> DeleteUser(BaseUserDTO dto)
        {
            var user = await this._userRepository.GetById(dto.Id);

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            this._userRepository.Delete(user);

            return await this._userRepository.SaveAsync();
        }

        public async Task<int> ChangePassword(ChangePasswordDTO dto)
        {
            var user = await this._userRepository.GetById(dto.Id);

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.UpdatePassword(dto.Password);

            return await this._userRepository.SaveAsync();
        }

        public async Task<ICollection<UserDetailsDTO>> GetAllUsers()
        {
            var users = await this._userRepository.GetAll()
                .Include(x => x.Role)
                .Where(x => x.Role.Name.Equals(Constants.USER_ROLE_NAME))
                .AsNoTracking()
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

        public async Task<UserDetailsDTO> BlockUser(BaseUserDTO dto)
        {
            var user = await this._userRepository.GetById(dto.Id);

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.Block();

            _userRepository.Update(user);

            await this._userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                IsBlocked = user.IsBlocked
            };

            return result;
        }

        public async Task<UserDetailsDTO> UnblockUser(BaseUserDTO dto)
        {
            var user = await this._userRepository.GetById(dto.Id);

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.Unblock();

            _userRepository.Update(user);

            await this._userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                IsBlocked = user.IsBlocked
            };

            return result;
        }        
    }
}

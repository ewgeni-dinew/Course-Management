namespace CourseManagement.Services
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
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
            var user = await Task.Run(() => this._userRepository.GetAll?
                .Include(x => x.Role)
                .Where(x => !x.IsBlocked)
                .FirstOrDefault(x => x.Username.Equals(dto.Username) && x.Password.Equals(dto.Password))
            );

            if (user == null)
            {
                throw new CustomException(ErrorMessages.INVALID_USER_CREDENTIALS);
            }

            var jwtToken = GenerateJWTToken(user);

            RevokePreviousUserTokens(user.RefreshTokens);

            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);

            await _userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                AccessToken = jwtToken,
                Role = user.Role.Name,
                RefreshToken = refreshToken.Token,
                GeoLat = user.Geo_Lat,
                GeoLng = user.Geo_Lng
            };

            return result;
        }

        public async Task<UserDetailsDTO> RegisterUser(RegisterUserDTO dto)
        {
            if (this._userRepository.GetAll.AsNoTracking().Any(x => x.Username.Equals(dto.Username)))
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

            if (user == null) //invalid input for user
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }
            else if (!user.Password.Equals(dto.Password)) //the input for current Password does not match DB password -> cannot update password
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.UpdatePassword(dto.NewPassword);

            return await this._userRepository.SaveAsync();
        }

        public async Task<GeoLocationDTO> SetGeoLocation(GeoLocationDTO dto, int userId)
        {
            if (!userId.Equals(dto.UserId))
            {
                throw new CustomException(ErrorMessages.NO_PERMISSIONS_FOR_ACTION);
            }

            var user = await this._userRepository.GetById(dto.UserId);

            if (user == null) //invalid input for user
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            user.UpdateGeoLocation(dto.GeoLat, dto.GeoLng);

            await this._userRepository.SaveAsync();

            var res = new GeoLocationDTO
            {
                GeoLat = (decimal)user.Geo_Lat,
                GeoLng = (decimal)user.Geo_Lng
            };

            return res;
        }

        public async Task<ICollection<UserDetailsDTO>> GetAllUsers()
        {
            var users = await this._userRepository.GetAll
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

        public async Task<ICollection<GeoLocationDTO>> GetContributors()
        {
            var contributors = await this._userRepository.GetAll
                .Where(x => x.Geo_Lat != null && x.Geo_Lng != null)
                .AsNoTracking()
                .Select(x => new GeoLocationDTO
                {
                    GeoLat = (decimal)x.Geo_Lat,
                    GeoLng = (decimal)x.Geo_Lng,
                })
                .ToListAsync();

            return contributors;
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

            this._userRepository.Update(user);

            await this._userRepository.SaveAsync();

            var result = new UserDetailsDTO
            {
                Id = user.Id,
                IsBlocked = user.IsBlocked
            };

            return result;
        }

        public async Task<RefreshTokenDTO> RefreshToken(RefreshTokenDTO dto)
        {
            var user = this._userRepository.GetAll
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id.Equals(dto.UserId));

            if (user == null)
            {
                throw new CustomException(ErrorMessages.ERROR_PROCESSING_DATA);
            }

            var token = user.RefreshTokens
                .FirstOrDefault(x => x.Token == dto.RefreshToken);

            if (token == null || !token.IsActive)
            {
                throw new CustomException(ErrorMessages.INVALID_REFRESH_TOKEN);
            }

            var newRefreshToken = GenerateRefreshToken();

            token.Revoked = DateTime.UtcNow;
            token.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            this._userRepository.Update(user);
            await this._userRepository.SaveAsync();

            var jwtToken = GenerateJWTToken(user);

            return new RefreshTokenDTO
            {
                AccessToken = jwtToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        public async Task<string> RevokeToken(RefreshTokenDTO dto)
        {
            var user = this._userRepository.GetAll
                .FirstOrDefault(x => x.Id.Equals(dto.UserId));

            if (user == null)
            {
                throw new CustomException(ErrorMessages.ERROR_PROCESSING_DATA);
            }

            var token = user.RefreshTokens
                .FirstOrDefault(x => x.Token == dto.RefreshToken);

            if (token == null)
            {
                throw new CustomException(ErrorMessages.INVALID_REFRESH_TOKEN);
            }

            if (!token.IsActive) return null;

            token.Revoked = DateTime.UtcNow;

            this._userRepository.Update(user);
            await this._userRepository.SaveAsync();

            return "Token was successfully revoked.";
        }

        //
        // *** PRIVATE METHODS ***
        //

        private void RevokePreviousUserTokens(ICollection<RefreshToken> refreshTokens)
        {
            var currentDate = DateTime.UtcNow;

            foreach (var t in refreshTokens.Where(x => x.IsActive))
            {
                t.Revoked = currentDate;
            }
        }

        private RefreshToken GenerateRefreshToken()
        {
            using var rng = new RNGCryptoServiceProvider();
            var byteBuffer = new byte[64];

            rng.GetBytes(byteBuffer); //fills the byte array

            return new RefreshToken
            {
                Token = Convert.ToBase64String(byteBuffer),
                Expires = DateTime.UtcNow.AddDays(7), //set the RefreshToken expiration date
                Created = DateTime.UtcNow,
                //TODO add IP address
            };
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
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
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

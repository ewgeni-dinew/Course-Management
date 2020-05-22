namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CouseManagement.DTO;
    using CouseManagement.DTO.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var user = await this._dbContext.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Username.Equals(dto.Username) && x.Password.Equals(dto.Password));

            if (user == null)
            {
                //throw expection;
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
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = tokenHandler.WriteToken(token);

            await _dbContext.SaveChangesAsync();

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

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            if (this._dbContext.Users.Any(x => x.Username.Equals(dto.Username)))
            {
                //throw error;
            }

            var user = new ApplicationUser
            {
                Username = dto.Username,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = 1
            };

            this._dbContext.Users.Add(user);

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserDTO dto)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (user == null)
            {
                //throw expection;
            }

            if (dto.Password != "")
            {
                user.Password = dto.Password;
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            await this._dbContext.SaveChangesAsync();

            var result = new UserDetailsDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
            };

            return Ok(result);
        }
    }
}
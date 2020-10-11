namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Services.Contracts;
    using CouseManagement.DTO.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var res = await this._userService.LoginUser(dto);

            return Ok(res);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAll()
        {
            var res = await this._userService.GetAllUsers();

            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Block(BaseUserDTO dto)
        {
            var user = await this._dbContext.Users
                .FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (user == null)
            {
                //throw exception;
            }

            user.IsBlocked = true;

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Unblock(BaseUserDTO dto)
        {
            var user = await this._dbContext.Users
                .FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (user == null)
            {
                //throw exception;
            }

            user.IsBlocked = false;

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(BaseUserDTO dto)
        {
            await this._userService.DeleteUser(dto);

            return Ok();
        }
    }
}
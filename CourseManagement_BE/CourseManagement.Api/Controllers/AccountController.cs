namespace CourseManagement.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CourseManagement.DTO.Account;
    using CourseManagement.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Linq;

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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var res = await this._userService.LoginUser(dto);

            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            await this._userService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserDTO dto)
        {
            var res = await this._userService.UpdateUser(dto);

            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            await this._userService.ChangePassword(dto);

            return Ok();
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
            await this._userService.BlockUser(dto);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Unblock(BaseUserDTO dto)
        {
            await this._userService.UnblockUser(dto);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(BaseUserDTO dto)
        {
            await this._userService.DeleteUser(dto);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken(RefreshTokenDTO dto)
        {
            var res = await this._userService.RefreshToken(dto);

            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RevokeToken(RefreshTokenDTO dto)
        {
            var res = await this._userService.RevokeToken(dto);

            return Ok(res);
        }
    }
}
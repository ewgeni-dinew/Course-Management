namespace CourseManagement.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CourseManagement.DTO.Account;
    using CourseManagement.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using System;
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

        [HttpPost("{userId}")] //TODO try it out with post
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken(int userId, [FromBody] RefreshTokenDTO dto)
        {
            //var cookie = Request.Cookies["refreshToken"];

            var refreshToken = dto.RefreshToken;

            var res = await this._userService.RefreshToken(userId, refreshToken);

            SetRefreshTokenCookie(res.Item2);

            return Ok(res.Item1);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RevokeToken(RefreshTokenDTO dto)
        {
            //var refreshToken = Request.Cookies["refreshToken"];

            var refreshToken = dto.RefreshToken;

            var userId = GetUserIdFromJWT();

            var res = await this._userService.RevokeToken(userId, refreshToken);

            return Ok(res);
        }


        //
        // Private methods
        //

        private int GetUserIdFromJWT()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            return int.Parse(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
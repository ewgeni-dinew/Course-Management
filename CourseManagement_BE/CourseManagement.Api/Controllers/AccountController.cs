namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Services.Contracts;
    using CouseManagement.DTO.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
    }
}
namespace CourseManagement.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login()
        {
            return Ok("This is account login!");
        }

        [HttpPost]
        public IActionResult Register()
        {
            return Ok("This is account register!");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return Ok("This is account logout!");
        }

        [HttpPost]
        public IActionResult Update()
        {
            return Ok("This is account login!");
        }
    }
}
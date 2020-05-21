namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CouseManagement.DTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Linq;
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
        public IActionResult Login()
        {
            return Ok("This is account login!");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            if (this._dbContext.Users.Any(x=>x.Username.Equals(dto.Username)))
            {
                return Ok();
            }

            var user = new ApplicationUser
            {
                Username = dto.Username,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            this._dbContext.Users.Add(user);

            await this._dbContext.SaveChangesAsync();

            return Ok();
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
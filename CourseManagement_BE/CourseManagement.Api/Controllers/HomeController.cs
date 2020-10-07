
namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return Ok("This is home!");
        }
    }
}
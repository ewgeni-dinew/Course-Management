
namespace CourseManagement.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        public IActionResult Index()
        {
            return Ok("This is home!");
        }
    }
}
namespace CourseManagement.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("The application is listening on port :5000!");
        }
    }
}
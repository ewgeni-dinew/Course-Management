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

        //[HttpGet]
        //public async Task SSE()
        //{
        //    var response = HttpContext.Response;

        //    response.Headers.Add("Content-Type", "text/event-stream");

        //    while (true)
        //    {
        //        await response.WriteAsync($"data: at {DateTime.UtcNow} \n\n");

        //        await response.Body.FlushAsync();

        //        await Task.Delay(2000);
        //    }
        //}
    }
}
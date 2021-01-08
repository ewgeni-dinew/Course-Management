namespace CourseManagement.Api.Controllers
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("The application is listening on port :5000!");
        }

        [EnableCors]
        [HttpGet]
        public async Task SSE()
        {
            var response = HttpContext.Response;

            response.Headers.Add("Content-Type", "text/event-stream");

            while (true)
            {
                var dto = new
                {
                    data = DateTime.UtcNow.ToString()
                };

                await response.WriteAsync($"data: {JsonSerializer.Serialize(dto)} \n\n");

                await response.Body.FlushAsync();

                await Task.Delay(1000);
            }
        }
    }
}
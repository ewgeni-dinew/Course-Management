namespace CourseManagement.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using CourseManagement.DTO.Course;
    using CourseManagement.Services.Contracts;
    using CourseManagement.Utilities.Constants;
    using CourseManagement.Api.Authorization;
    using CourseManagement.Api.SignalR.Hubs;
    using CourseManagement.Api.Helpers;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IHubContext<CourseManagementHub> _hubContext;

        public CourseController(ICourseService courseService, IHubContext<CourseManagementHub> hubContext)
        {
            this._courseService = courseService;
            this._hubContext = hubContext;
        }

        [HttpPost]
        [CustomAuthorization("Admin")]
        public async Task<IActionResult> Create(CreateCourseDTO dto)
        {
            var res = await this._courseService.CreateCourse(dto);

            await this._hubContext.Clients.All.SendCoreAsync("addCourse", new object[] { res });

            return Ok();
        }

        [HttpPost]
        [CustomAuthorization("Admin")]
        public async Task<IActionResult> Edit(EditCourseDTO dto)
        {
            await this._courseService.EditCourse(dto);

            await this._hubContext.Clients.All.SendCoreAsync("editCourse", null);

            return Ok();
        }


        [HttpPost]
        [CustomAuthorization("Admin")]
        public async Task<IActionResult> Delete(DeleteCourseDTO dto)
        {
            await this._courseService.DeleteCourse(dto);

            await this._hubContext.Clients.All.SendCoreAsync("deleteCourse", null);

            return Ok();
        }

        [HttpGet]
        [CustomAuthorization()]
        public async Task<IActionResult> GetAll()
        {
            var res = await this._courseService.GetAllCourses();

            return Ok(res);
        }

        [HttpGet]
        [CustomAuthorization()]
        public async Task<IActionResult> GetAllUserCourses()
        {
            var userId = JwtExtractor.GetUserId(HttpContext.User.Identity as ClaimsIdentity);

            var res = await this._courseService.GetAllUserCourses(userId);

            return Ok(res);
        }

        [HttpGet]
        [CustomAuthorization()]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = JwtExtractor.GetUserId(HttpContext.User.Identity as ClaimsIdentity);

            var res = await this._courseService.GetFavoriteCourses(userId);

            return Ok(res);
        }

        [HttpPost]
        [CustomAuthorization()]
        public async Task<IActionResult> AddToFavorites(AddToFavoritesDTO dto)
        {
            var userId = JwtExtractor.GetUserId(HttpContext.User.Identity as ClaimsIdentity);

            var res = await this._courseService.AddToFavorites(dto, userId);

            return Ok(res);
        }

        [HttpPost]
        [CustomAuthorization()]
        public async Task<IActionResult> RemoveFromFavorites(AddToFavoritesDTO dto)
        {
            var userId = JwtExtractor.GetUserId(HttpContext.User.Identity as ClaimsIdentity);

            var res = await this._courseService.RemoveFromFavorites(dto, userId);

            return Ok(res);
        }

        [HttpGet("{id}")]
        [CustomAuthorization()]
        public async Task<IActionResult> Details(int id)
        {
            var userId = JwtExtractor.GetUserId(HttpContext.User.Identity as ClaimsIdentity);

            var res = await this._courseService.GetCourseDetails(id, userId);

            return Ok(res);
        }

        [HttpPost]
        [CustomAuthorization()]
        public async Task<IActionResult> Rate(RateCourseDTO dto)
        {
            var res = await this._courseService.RateCourse(dto);

            await this._hubContext.Clients.All.SendCoreAsync("rateCourse", null);

            return Ok(res);
        }

        [HttpPost]
        [CustomAuthorization()]
        public async Task<IActionResult> ChangeCourseState(ChangeCourseStateDTO dto)
        {
            var res = await this._courseService.ChangeUserCourseState(dto);

            return Ok(res);
        }

        [HttpGet("{id}")]
        [CustomAuthorization()]
        public async Task<IActionResult> DownloadPDF(int id)
        {
            var courseKVP = await this._courseService.DownloadCourseAsPDF(id);

            return File(courseKVP.Value, Constants.APPLICATION_PDF_MIME, $"{courseKVP.Key}{Constants.FILE_EXTENSION_PDF}");
        }


        [HttpGet("{id}")]
        [CustomAuthorization()]
        public async Task<IActionResult> DownloadWord(int id)
        {
            var courseKVP = await this._courseService.DownloadCourseAsWORD(id);

            return File(courseKVP.Value, Constants.APPLICATION_WORD_MIME, $"{courseKVP.Key}{Constants.FILE_EXTENSION_WORD}");
        }
    }
}

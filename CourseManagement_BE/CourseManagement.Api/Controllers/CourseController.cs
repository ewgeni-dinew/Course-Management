namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CouseManagement.DTO.Course;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCourseDTO dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                Summary = dto.Summary,
            };

            this._dbContext.Courses.Add(course);

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditCourseDTO dto)
        {
            var course = await this._dbContext.Courses
                .FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (course == null)
            {
                //throw exception
            }

            course.Title = dto.Title;
            course.Content = dto.Content;

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteCourseDTO dto)
        {
            var course = await this._dbContext.Courses
                .FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (course == null)
            {
                //throw exception
            }

            this._dbContext.Courses.Remove(course);

            await this._dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var courses = await this._dbContext.Courses
                .Select(x => new BaseCourseDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary
                    //CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
                })
                .ToListAsync();

            return Ok(courses);
        }
    }
}

namespace CourseManagement.Api.Controllers
{
    using CourseManagement.Data;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Security.Claims;
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
            course.Summary = dto.Summary;

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
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserIdFromJWT();

            var courses = await this._dbContext.FavoriteCourses
                .Include(x => x.Course)
                .Where(x => x.UserId.Equals(userId))
                .Select(x => new BaseCourseDTO
                {
                    Id = x.Course.Id,
                    Title = x.Course.Title,
                    Summary = x.Course.Summary
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToFavorites(AddToFavoritesDTO dto)
        {
            var userId = GetUserIdFromJWT();

            this._dbContext.FavoriteCourses.Add(new FavoriteCourse
            {
                CourseId = dto.CourseId,
                UserId = userId
            });

            return Ok(await this._dbContext.SaveChangesAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromFavorites(AddToFavoritesDTO dto)
        {
            var userId = GetUserIdFromJWT();

            var favoriteCourse = await this._dbContext.FavoriteCourses
                .FirstOrDefaultAsync(x => x.CourseId.Equals(dto.CourseId) && x.UserId.Equals(userId));

            this._dbContext.FavoriteCourses.Remove(favoriteCourse);

            return Ok(await this._dbContext.SaveChangesAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserIdFromJWT();

            var course = await this._dbContext.Courses
                .Include(x => x.Author)
                .Include(x => x.Favorites)
                .Select(x => new CourseDetailsDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    Content = x.Content,
                    Rating = x.Rating,
                    Author = $"{x.Author.FirstName} {x.Author.LastName}",
                    IsFavorite = x.Favorites.Any(x => x.UserId.Equals(userId))
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return Ok(course);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Rate(RateCourseDTO dto)
        {
            var course = await this._dbContext.Courses
                .FirstOrDefaultAsync(x => x.Id.Equals(dto.CourseId));

            if (course == null)
            {
                //throw exception
            };

            if (course.Rating.Equals(0)) //course has not been rated so far
            {
                course.Rating = dto.Rating;
            }
            else
            {
                course.Rating = Math.Round((course.Rating + dto.Rating) / 2, 2);
            }

            await this._dbContext.SaveChangesAsync();

            return Ok(new CourseDetailsDTO
            {
                Rating = course.Rating
            });
        }

        private int GetUserIdFromJWT()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            return int.Parse(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}

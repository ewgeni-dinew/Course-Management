namespace CourseManagement.Services
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseService : ICourseService
    {
        private readonly ICourseFactory _courseFactory;
        private readonly IFavoriteCourseFactory _favoriteCourseFactory;

        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<FavoriteCourse> _favCourseRepository;

        public CourseService(
            ICourseFactory courseFactory,
            IFavoriteCourseFactory favoriteCourseFactory,
            IRepository<Course> courseRepository,
            IRepository<FavoriteCourse> favCourseRepository
            )
        {
            _courseFactory = courseFactory;
            _courseRepository = courseRepository;
            _favCourseRepository = favCourseRepository;
            _favoriteCourseFactory = favoriteCourseFactory;
        }

        public async Task CreateCourse(CreateCourseDTO dto)
        {
            var course = this._courseFactory
                .WithTitle(dto.Title)
                .WithSummary(dto.Summary)
                .WithContent(dto.Content)
                .WithAuthorId(dto.AuthorId)
                .Build();

            this._courseRepository.Create(course);

            await this._courseRepository.SaveAsync();
        }

        public async Task EditCourse(EditCourseDTO dto)
        {
            var course = await this._courseRepository.GetById(dto.Id);

            if (course == null)
            {
                //throw exception
            }

            course.UpdateTitle(dto.Title);
            course.UpdateContent(dto.Content);
            course.UpdateSummary(dto.Summary);

            this._courseRepository.Update(course);

            await this._courseRepository.SaveAsync();
        }

        public async Task DeleteCourse(DeleteCourseDTO dto)
        {
            var course = await this._courseRepository.GetById(dto.Id);

            if (course == null)
            {
                //throw exception
            }

            this._courseRepository.Delete(course);

            await this._courseRepository.SaveAsync();
        }

        public async Task<CourseDetailsDTO> GetCourseDetails(int id, int userId)
        {
            var course = await this._courseRepository.GetAll()
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
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return course;
        }

        public async Task<ICollection<BaseCourseDTO>> GetAllCourses()
        {
            var courses = await this._courseRepository.GetAll()
                .Select(x => new BaseCourseDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary
                })
                .ToListAsync();

            return courses;
        }

        public async Task<ICollection<BaseCourseDTO>> GetFavoriteCourses(int userId)
        {
            var courses = await this._favCourseRepository.GetAll()
                .Include(x => x.Course)
                .Where(x => x.UserId.Equals(userId))
                .Select(x => new BaseCourseDTO
                {
                    Id = x.Course.Id,
                    Title = x.Course.Title,
                    Summary = x.Course.Summary
                })
                .ToListAsync();

            return courses;
        }

        public async Task<CourseRatingDTO> RateCourse(RateCourseDTO dto)
        {
            var course = await this._courseRepository.GetById(dto.CourseId);

            if (course == null)
            {
                //throw exception
            }
            else if (course.Rating.Equals(0)) //course has not been rated so far
            {
                course.UpdateRating(dto.Rating); //set initial rating
            }
            else
            {
                course.UpdateRating(Math.Round((course.Rating + dto.Rating) / 2, 2)); //calculate the average rating
            }

            await this._courseRepository.SaveAsync();

            return new CourseRatingDTO
            {
                CourseId = dto.CourseId,
                Rating = course.Rating
            };
        }

        public async Task<int> RemoveFromFavorites(AddToFavoritesDTO dto, int userId)
        {
            var favoriteCourse = await this._favCourseRepository.GetAll()
                .FirstOrDefaultAsync(x => x.CourseId.Equals(dto.CourseId) && x.UserId.Equals(userId));

            this._favCourseRepository.Delete(favoriteCourse);

            return await this._favCourseRepository.SaveAsync();
        }

        public async Task<int> AddToFavorites(AddToFavoritesDTO dto, int userId)
        {
            var favCourse = this._favoriteCourseFactory
                .WithCourseId(dto.CourseId)
                .WithUserId(userId)
                .Build();

            this._favCourseRepository.Create(favCourse);

            return await this._favCourseRepository.SaveAsync();
        }
    }
}

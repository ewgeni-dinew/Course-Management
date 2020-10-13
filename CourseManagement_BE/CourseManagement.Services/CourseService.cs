namespace CourseManagement.Services
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
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

        public Task GetCourseDetails(int id)
        {
            throw new System.NotImplementedException();
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

        public Task<ICollection<BaseCourseDTO>> GetFavoriteCourses()
        {
            throw new System.NotImplementedException();
        }

        public Task RateCourse(RateCourseDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromFavorites(AddToFavoritesDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task AddToFavorites(AddToFavoritesDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}

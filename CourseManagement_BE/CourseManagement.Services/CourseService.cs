namespace CourseManagement.Services
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using System.Collections.Generic;
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

        public Task AddToFavorites(AddToFavoritesDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void CreateCourse(CreateCourseDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCourse(DeleteCourseDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void EditCourse(EditCourseDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BaseCourseDTO>> GetAllCourses()
        {
            throw new System.NotImplementedException();
        }

        public Task GetCourseDetails(int id)
        {
            throw new System.NotImplementedException();
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
    }
}

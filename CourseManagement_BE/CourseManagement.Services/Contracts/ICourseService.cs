namespace CourseManagement.Services.Contracts
{
    using CourseManagement.DTO.Course;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        public Task CreateCourse(CreateCourseDTO dto);

        public Task EditCourse(EditCourseDTO dto);

        public Task DeleteCourse(DeleteCourseDTO dto);

        public Task<CourseDetailsDTO> GetCourseDetails(int id, int userId);

        public Task<ICollection<BaseCourseDTO>> GetAllCourses();

        public Task<ICollection<BaseCourseDTO>> GetFavoriteCourses(int userId);

        public Task<int> AddToFavorites(AddToFavoritesDTO dto, int userId);

        public Task<int> RemoveFromFavorites(AddToFavoritesDTO dto, int userId);

        public Task<CourseRatingDTO> RateCourse(RateCourseDTO dto);
    }
}

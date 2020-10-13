namespace CourseManagement.Services.Contracts
{
    using CourseManagement.DTO.Course;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        public void CreateCourse(CreateCourseDTO dto);

        public void EditCourse(EditCourseDTO dto);

        public Task DeleteCourse(DeleteCourseDTO dto);

        public Task GetCourseDetails(int id);

        public Task<ICollection<BaseCourseDTO>> GetAllCourses();

        public Task<ICollection<BaseCourseDTO>> GetFavoriteCourses();

        public Task AddToFavorites(AddToFavoritesDTO dto);

        public Task RemoveFromFavorites(AddToFavoritesDTO dto);

        public Task RateCourse(RateCourseDTO dto);
    }
}

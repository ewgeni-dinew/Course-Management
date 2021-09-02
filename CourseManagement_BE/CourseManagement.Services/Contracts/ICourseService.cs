namespace CourseManagement.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CourseManagement.DTO.Course;

    public interface ICourseService
    {
        public Task<CourseDetailsDTO> CreateCourse(CreateCourseDTO dto);

        public Task<CourseDetailsDTO> EditCourse(EditCourseDTO dto);

        public Task<int> DeleteCourse(DeleteCourseDTO dto);

        public Task<CourseDetailsDTO> GetCourseDetails(int id, int userId);

        public Task<ICollection<BaseCourseDTO>> GetAllCourses();

        public Task<ICollection<UserCourseDTO>> GetAllUserCourses(int userId);

        public Task<ICollection<BaseCourseDTO>> GetFavoriteCourses(int userId);

        public Task<int> AddToFavorites(AddToFavoritesDTO dto, int userId);

        public Task<int> RemoveFromFavorites(AddToFavoritesDTO dto, int userId);

        public Task<int> ChangeUserCourseState(ChangeCourseStateDTO dto);

        public Task<CourseRatingDTO> RateCourse(RateCourseDTO dto);

        public Task<KeyValuePair<string, byte[]>> DownloadCourseAsPDF(int id);

        public Task<KeyValuePair<string, byte[]>> DownloadCourseAsWORD(int id);
    }
}

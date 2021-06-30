namespace CourseManagement.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Text;
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services.Contracts;
    using CourseManagement.Utilities.Errors;
    using CourseManagement.Utilities.Constants;
    using Microsoft.EntityFrameworkCore;
    using CourseManagement.Services.Utils.PDF;
    using CourseManagement.Services.Utils.Word;

    /// <summary>
    /// The class is part of the application Service layer. It handles all the Business Logic connected to the Course and FavoriteCourse logical spaces.
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly IPdfService _pdfService;
        private readonly IWordService _wordService;

        private readonly ICourseFactory _courseFactory;
        private readonly IFavoriteCourseFactory _favCourseFactory;

        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<FavoriteCourse> _favCourseRepository;

        public CourseService(
            IPdfService pdfService,
            IWordService wordService,
            ICourseFactory courseFactory,
            IFavoriteCourseFactory favoriteCourseFactory,
            IRepository<Course> courseRepository,
            IRepository<FavoriteCourse> favCourseRepository)
        {
            this._pdfService = pdfService;
            this._wordService = wordService;
            this._courseFactory = courseFactory;
            this._courseRepository = courseRepository;
            this._favCourseRepository = favCourseRepository;
            this._favCourseFactory = favoriteCourseFactory;
        }

        public async Task<CourseDetailsDTO> CreateCourse(CreateCourseDTO dto)
        {
            var course = this._courseFactory
                .WithTitle(dto.Title)
                .WithSummary(dto.Summary)
                .WithContent(dto.Content)
                .WithAuthorId(dto.AuthorId)
                .Build();

            this._courseRepository.Create(course);

            await this._courseRepository.SaveAsync();

            var result = new CourseDetailsDTO
            {
                Id = course.Id,
                Title = course.Title,
                Summary = course.Summary,
                Content = course.Content
            };

            return result;
        }

        public async Task<CourseDetailsDTO> EditCourse(EditCourseDTO dto)
        {
            var course = await this._courseRepository.GetById(dto.Id);

            if (course == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            course.UpdateTitle(dto.Title);
            course.UpdateContent(dto.Content);
            course.UpdateSummary(dto.Summary);

            this._courseRepository.Update(course);

            await this._courseRepository.SaveAsync();

            var result = new CourseDetailsDTO
            {
                Id = course.Id,
                Title = course.Title,
                Summary = course.Summary,
                Content = course.Content
            };

            return result;
        }

        public async Task<int> DeleteCourse(DeleteCourseDTO dto)
        {
            var course = await this._courseRepository.GetById(dto.Id);

            if (course == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            this._courseRepository.Delete(course);

            return await this._courseRepository.SaveAsync();
        }

        public async Task<CourseDetailsDTO> GetCourseDetails(int id, int userId)
        {
            var course = await this._courseRepository.GetAll
                .Include(x => x.Author)
                .Include(x => x.Favorites)
                .Select(x => new CourseDetailsDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Summary = x.Summary,
                    CreatedOn = x.CreatedOn.ToString(Constants.DATETIME_PRECISION_FORMAT),
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
            var courses = await this._courseRepository.GetAll
                .AsNoTracking()
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
            var courses = await this._favCourseRepository.GetAll
                .Include(x => x.Course)
                .Where(x => x.UserId.Equals(userId))
                .AsNoTracking()
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
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA); //Course is not valid
            }
            else if (course.Rating.Equals(0)) //Course has not been rated so far
            {
                course.UpdateRating(dto.Rating); //sets initial Course rating
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
            var favoriteCourse = await this._favCourseRepository.GetAll
                .FirstOrDefaultAsync(x => x.CourseId.Equals(dto.CourseId) && x.UserId.Equals(userId));

            if (favoriteCourse == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
            }

            this._favCourseRepository.Delete(favoriteCourse);

            return await this._favCourseRepository.SaveAsync();
        }

        public async Task<int> AddToFavorites(AddToFavoritesDTO dto, int userId)
        {
            var favCourse = this._favCourseFactory
                .WithCourseId(dto.CourseId)
                .WithUserId(userId)
                .Build();

            this._favCourseRepository.Create(favCourse);

            return await this._favCourseRepository.SaveAsync();
        }

        public async Task<KeyValuePair<string, byte[]>> DownloadCourseAsPDF(int courseId)
        {
            var course = await this._courseRepository.GetById(courseId);

            if (course == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA); //Course is not valid
            }
            else if (course.UpdatedOn?.ToString(Constants.DATETIME_PRECISION_F_FORMAT)
                != course.ContentBytesCreatedOn?.ToString(Constants.DATETIME_PRECISION_F_FORMAT))
            { //bytes content is NOT from the latest version of the course data; update bytes
                await UpdateCourseByteArrayContent(course);
            }

            var courseAsByteArray = this._pdfService.GeneratePdfFile(course.Title, course.Content);

            var kvp = new KeyValuePair<string, byte[]>(course.Title.ToLower(), courseAsByteArray);

            return kvp;
        }

        public async Task<KeyValuePair<string, byte[]>> DownloadCourseAsWORD(int courseId)
        {
            var course = await this._courseRepository.GetById(courseId);

            if (course == null)
            {
                throw new CustomException(ErrorMessages.INVALID_INPUT_DATA); //Course is not valid
            }
            else if (course.UpdatedOn?.ToString(Constants.DATETIME_PRECISION_F_FORMAT)
                != course.ContentBytesCreatedOn?.ToString(Constants.DATETIME_PRECISION_F_FORMAT))
            { //bytes content is NOT from the latest version of the course data; update bytes
                await UpdateCourseByteArrayContent(course);
            }

            var courseAsByteArray = this._wordService.GenerateWordFile(course.Title, course.Content);

            var kvp = new KeyValuePair<string, byte[]>(course.Title, courseAsByteArray);

            return kvp;
        }

        private async Task UpdateCourseByteArrayContent(Course course)
        {
            var sb = new StringBuilder();

            //append course data to StringBuilder; add two empty lines between Title and Content
            sb.AppendLine(course.Title).AppendLine().AppendLine().AppendLine(course.Content);

            var bytes = Encoding.ASCII.GetBytes(sb.ToString());

            course.UpdateContentBytes(bytes);

            await this._courseRepository.SaveAsync();
        }
    }
}

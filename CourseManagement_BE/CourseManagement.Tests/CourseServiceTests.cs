namespace CourseManagement.Tests
{
    using CourseManagement.Data;
    using CourseManagement.Data.Factories;
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Course;
    using CourseManagement.Repository;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services;
    using CourseManagement.Services.Contracts;
    using CourseManagement.Services.Utils.PDF;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using DinkToPdf;
    using Xunit;
    using CourseManagement.Services.Utils.Word;

    public class CourseServiceTests
    {
        private readonly IPdfService _pdfService;
        private readonly IWordService _wordService;

        private readonly ICourseFactory _courseFactory;
        private readonly IFavoriteCourseFactory _favoriteCourseFactory;
        private readonly IUserFactory _userFactory;
        private readonly IUserCourseFactory _userCourseFactory;

        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<FavoriteCourse> _favCourseRepository;
        private readonly IRepository<UserCourse> _userCourseRepository;

        private readonly ICourseService _courseService;

        public CourseServiceTests()
        {
            this._courseFactory = new CourseFactory();
            this._favoriteCourseFactory = new FavoriteCourseFactory();
            this._userFactory = new UserFactory();
            this._userCourseFactory = new UserCourseFactory();

            var dbContext = SetupMockDatabaseWithSeedData();

            this._courseRepository = new CourseRepository(dbContext);

            this._favCourseRepository = new FavoriteCourseRepository(dbContext);

            this._userCourseRepository = new UserCourseRepository(dbContext);

            this._pdfService = new PdfService(new SynchronizedConverter(new PdfTools()));

            this._wordService = new WordService();

            this._courseService = new CourseService(
                this._pdfService,
                this._wordService,
                this._courseFactory,
                this._favoriteCourseFactory,
                this._userCourseFactory,
                this._courseRepository,
                this._favCourseRepository,
                this._userCourseRepository);
        }

        [Fact]
        public void CreateCourse_WithValidInput()
        {
            var dto = new CreateCourseDTO
            {
                Title = "Title",
                Content = "Content",
                Summary = "Summary",
                AuthorId = 1
            };

            var res = this._courseService.CreateCourse(dto).Result;

            Assert.Equal(dto.Title, res.Title);
            Assert.Equal(dto.Summary, res.Summary);
            Assert.Equal(dto.Content, res.Content);
        }

        [Fact]
        public void EditCourse_WithValidInput()
        {
            var dto = new EditCourseDTO
            {
                Id = 1,
                Title = "Title_UPDATED",
                Summary = "Summary_UPDATED",
                Content = "Content_UPDATED"
            };

            var res = this._courseService.EditCourse(dto).Result;

            Assert.Equal(dto.Title, res.Title);
            Assert.Equal(dto.Summary, res.Summary);
            Assert.Equal(dto.Content, res.Content);
        }

        [Fact]
        public void DeleteCourse_WithValidInput()
        {
            var dto = new DeleteCourseDTO
            {
                Id = 2
            };

            var res = this._courseService.DeleteCourse(dto).Result;

            Assert.NotEqual(0, res);
        }

        [Fact]
        public void GetCourseDetails_WithValidInput()
        {
            var userId = 1;
            var courseId = 1;

            var res = this._courseService.GetCourseDetails(courseId, userId).Result;

            Assert.NotNull(res);
        }

        [Fact]
        public void GetAllCourses_WithValidInput()
        {
            var res = this._courseService.GetAllCourses().Result;

            Assert.NotEqual(0, res.Count);
        }

        [Fact]
        public void AddCourseToFavorites_WithValidInput()
        {
            var userId = 1;

            var initialFavCourseCount = this._courseService.GetFavoriteCourses(userId).Result.Count;

            var dto = new AddToFavoritesDTO
            {
                CourseId = 5
            };

            this._courseService.AddToFavorites(dto, userId).Wait();

            var favCourses = this._courseService.GetFavoriteCourses(userId).Result;

            Assert.Equal(initialFavCourseCount + 1, favCourses.Count);
        }

        [Fact]
        public void RemoveCourseFromFavorites_WithValidInput()
        {
            var userId = 1;

            var initialFavCourseCount = this._courseService.GetFavoriteCourses(userId).Result.Count;

            var dto = new AddToFavoritesDTO
            {
                CourseId = 1
            };

            this._courseService.AddToFavorites(dto, userId).Wait();

            this._courseService.RemoveFromFavorites(dto, userId).Wait();

            var favCoursesCount = this._courseService.GetFavoriteCourses(userId).Result.Count;

            Assert.Equal(initialFavCourseCount, favCoursesCount);
        }

        [Fact]
        public void GetFavoriteCourses_WithValidInput()
        {
            var userId = 1;

            var res = this._courseService.GetFavoriteCourses(userId).Result;

            Assert.NotEqual(0, res.Count);
        }

        [Fact]
        public void RateCourse_WithValidInput()
        {
            short initialRating = 7;
            short secondRating = 3;
            var calcMedianRating = Math.Round((initialRating + secondRating) / 2.0, 2);

            var dto = new RateCourseDTO
            {
                CourseId = 1,
                Rating = initialRating
            };

            var res = this._courseService.RateCourse(dto).Result;

            Assert.Equal(initialRating, res.Rating); //assert initial rating is set to the first rating number

            dto.Rating = secondRating;
            res = this._courseService.RateCourse(dto).Result;

            Assert.Equal(calcMedianRating, res.Rating); //assert rating is calculated properly thereafter
        }

        [Fact]
        public void DownloadCourse_PDF_WithValidInput()
        {
            var userId = 1;
            var courseId = 1;

            var courseDto = this._courseService.GetCourseDetails(courseId, userId).Result;

            var res = this._courseService.DownloadCourseAsPDF(courseId).Result;

            Assert.Equal(courseDto.Title.ToLower(), res.Key.ToLower());
            Assert.NotNull(res.Value);
            Assert.NotEmpty(res.Value);
        }

        [Fact]
        public void DownloadCourse_WORD_WithValidInput()
        {
            var userId = 1;
            var courseId = 1;

            var courseDto = this._courseService.GetCourseDetails(courseId, userId).Result;

            var res = this._courseService.DownloadCourseAsWORD(courseId).Result;

            Assert.Equal(courseDto.Title.ToLower(), res.Key.ToLower());
            Assert.NotNull(res.Value);
            Assert.NotEmpty(res.Value);
        }

        //SETUP METHODS 

        private ApplicationDbContext SetupMockDatabaseWithSeedData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var user = this.BuildSingleUser(1, "username@test.com", 1);
            var courses = this.BuildCoursesCollection();
            var favCourses = this.BuildFavoriteCoursesCollection();

            dbContext.Users.Add(user);
            dbContext.Courses.AddRange(courses);
            dbContext.FavoriteCourses.AddRange(favCourses);

            dbContext.SaveChanges();

            return dbContext;
        }

        private ICollection<Course> BuildCoursesCollection()
        {
            return new List<Course>
            {
                this.BuildSingleCourse(1, "Title_1", "Summary_1", "Content_1", 1), //used for EditCourse test & RemoveCourseFromFavorites
                this.BuildSingleCourse(2, "Title_2", "Summary_2", "Content_2", 1), //used for DeleteCourse test
                this.BuildSingleCourse(3, "Title_3", "Summary_3", "Content_3", 1), //used for GetCourseFavorites test
                this.BuildSingleCourse(4, "Title_4", "Summary_4", "Content_4", 1), //used for GetCourseFavorites test
                this.BuildSingleCourse(5, "Title_5", "Summary_5", "Content_5", 1), //used for Add & Remove CourseFromFavorites test
            };
        }

        private ICollection<FavoriteCourse> BuildFavoriteCoursesCollection()
        {
            return new List<FavoriteCourse>
            {
                this.BuildSingleFavoriteCourse(1, 1, 3), //Course (Id = 3) is added to favorites for User (Id = 1)
                this.BuildSingleFavoriteCourse(2, 1, 4), //Course (Id = 4) is added to favorites for User (Id = 1)
            };
        }

        private Course BuildSingleCourse(int id, string title, string summary, string content, int authorId)
        {
            var course = this._courseFactory
                .WithId(id)
                .WithTitle(title)
                .WithSummary(summary)
                .WithContent(content)
                .WithAuthorId(authorId)
                .Build();

            return course;
        }

        private FavoriteCourse BuildSingleFavoriteCourse(int id, int userId, int courseId)
        {
            var favCourse = this._favoriteCourseFactory
                .WithId(id)
                .WithUserId(userId)
                .WithCourseId(courseId)
                .Build();

            return favCourse;
        }

        private ApplicationUser BuildSingleUser(int id, string username, int roleId)
        {
            var user = this._userFactory
               .WithId(id)
               .WithUsername(username)
               .WithRoleId(roleId)
               .Build();

            return user;
        }
    }
}

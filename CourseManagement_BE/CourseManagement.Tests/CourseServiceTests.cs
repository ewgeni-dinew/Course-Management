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
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class CourseServiceTests
    {
        private readonly ICourseFactory _courseFactory;
        private readonly IFavoriteCourseFactory _favoriteCourseFactory;

        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<FavoriteCourse> _favCourseRepository;

        private readonly ICourseService _courseService;

        public CourseServiceTests()
        {
            this._courseFactory = new CourseFactory();

            this._favoriteCourseFactory = new FavoriteCourseFactory();

            var dbContext = SetupMockDatabaseWithSeedData();

            this._courseRepository = new CourseRepository(dbContext);

            this._favCourseRepository = new FavoriteCourseRepository(dbContext);

            this._courseService = new CourseService(this._courseFactory, this._favoriteCourseFactory, this._courseRepository, this._favCourseRepository);

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

            this._courseService.CreateCourse(dto);
        }

        [Fact]
        public void EditCourse_WithValidInput()
        {

        }

        [Fact]
        public void DeleteCourse_WithValidInput()
        {

        }

        [Fact]
        public void GetCourseDetails_WithValidInput()
        {

        }

        [Fact]
        public void GetAllCourses_WithValidInput()
        {

        }

        [Fact]
        public void AddCourseToFavorites_WithValidInput()
        {

        }

        [Fact]
        public void RemoveCourseFromFavorites_WithValidInput()
        {

        }

        [Fact]
        public void GetFavoriteCourses_WithValidInput()
        {

        }

        [Fact]
        public void RateCourse_WithValidInput()
        {

        }

        //SETUP METHODS 

        private ApplicationDbContext SetupMockDatabaseWithSeedData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            //var users = this.BuildUsersCollection();

            //dbContext.Roles.AddRange(roles);

            //dbContext.SaveChanges();

            return dbContext;
        }
    }
}

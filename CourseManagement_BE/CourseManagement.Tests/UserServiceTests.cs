namespace CourseManagement.Tests
{
    using CourseManagement.Data;
    using CourseManagement.Data.Factories;
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Account;
    using CourseManagement.Repository;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services;
    using CourseManagement.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UserServiceTests
    {
        private readonly IUserFactory userFactory;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IUserService userService;

        public UserServiceTests()
        {
            this.userFactory = new UserFactory();
            
            var dbContext = SetupMockDatabaseWithSeedData();

            this.userRepository = new UserRepository(dbContext.Object);

            this.userService = new UserService(this.userRepository, this.userFactory);
        }       

        [Fact]
        public void RegisterUser_WithValidInput()
        {
            var dto = new RegisterUserDTO
            {
                Username = "username@test.com",
                Password = "123456",
                FirstName = "FirstName",
                LastName = "Lastname",
            };

            var res = this.userService.RegisterUser(dto).Result;

            Assert.Equal(dto.Username, res.Username);
        }

        //register user with already taken username
        //register user with invalid input


        [Fact]
        public void BlockUser_ValidInput()
        {
            var dto = new BaseUserDTO
            {
                Id = 1,
            };

            var res = this.userService.BlockUser(dto).Result;

            Assert.Equal(true.ToString(), res.IsBlocked.ToString());
        }

        [Fact]
        public void UnblockUser_ValidInput()
        {
            var dto = new BaseUserDTO
            {
                Id = 1,
            };

            var res = this.userService.UnblockUser(dto).Result;

            Assert.Equal(false.ToString(), res.IsBlocked.ToString());
        }

        private Mock<ApplicationDbContext> SetupMockDatabaseWithSeedData()
        {
            var users = this.BuildUsersAsQueryable();

            var mockSet = new Mock<DbSet<ApplicationUser>>();
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());


            var dbContext = new Mock<ApplicationDbContext>();
            dbContext.Setup(x => x.Users).Returns(mockSet.Object);

            return dbContext;
        }

        private IQueryable<ApplicationUser> BuildUsersAsQueryable()
        {
            var users = new List<ApplicationUser>
            {
                this.BuildSingleUser(1, "Firstname", "Lastname", "username@test.com", "password", 1),
                this.BuildSingleUser(2, "Firstname", "Lastname", "username_2@test.com", "password", 1),
            };

            return users.AsQueryable();
        }

        private ApplicationUser BuildSingleUser(int id, string firstName, string lastName, string username, string password, int roleId)
        {
            var user = this.userFactory
               .WithId(id)
               .WithFirstName(firstName)
               .WithLastName(lastName)
               .WithUsername(username)
               .WithPassword(password)
               .WithRoleId(roleId)
               .Build();

            return user;
        }
    }
}

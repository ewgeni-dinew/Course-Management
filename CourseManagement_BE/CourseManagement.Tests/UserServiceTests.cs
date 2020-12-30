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
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class UserServiceTests
    {
        private readonly IUserFactory userFactory;
        private readonly IRoleFactory roleFactory;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IUserService userService;

        public UserServiceTests()
        {
            this.userFactory = new UserFactory();

            this.roleFactory = new RoleFactory();

            var dbContext = SetupMockDatabaseWithSeedData();

            this.userRepository = new UserRepository(dbContext);

            this.userService = new UserService(this.userRepository, this.userFactory);
        }

        [Fact]
        public void LoginUser_WithValidInput()
        {
            var dto = new LoginUserDTO
            {
                Username = "username@test.com",
                Password = "password"
            };

            var res = this.userService.LoginUser(dto).Result;

            Assert.NotEqual("", res.Token);
            Assert.Equal(1, res.Id);
            Assert.Equal(dto.Username, res.Username);
        }

        [Fact]
        public void RegisterUser_WithValidInput()
        {
            var dto = new RegisterUserDTO
            {
                Username = "username_test3213@test.com",
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

        //SETUP METHODS >>>

        private ApplicationDbContext SetupMockDatabaseWithSeedData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var users = this.BuildUsersCollection();

            var roles = this.BuildUserRolesCollection();

            dbContext.Users.AddRange(users);
            dbContext.Roles.AddRange(roles);

            dbContext.SaveChanges();

            return dbContext;
        }

        private ICollection<Role> BuildUserRolesCollection()
        {
            return new List<Role>
            {
                this.BuildSingleUserRole(1, "User"),
                this.BuildSingleUserRole(2, "Admin")
            };
        }

        private ICollection<ApplicationUser> BuildUsersCollection()
        {
            return new List<ApplicationUser>
            {
                this.BuildSingleUser(1, "Firstname", "Lastname", "username@test.com", "password", 1),
                this.BuildSingleUser(2, "Firstname", "Lastname", "username_2@test.com", "password", 1),
                this.BuildSingleUser(3, "Firstname", "Lastname", "username_3@test.com", "password", 1),
            };
        }

        private Role BuildSingleUserRole(int id, string name)
        {
            var role = this.roleFactory
                .WithId(id)
                .WithName(name)
                .Build();

            return role;
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

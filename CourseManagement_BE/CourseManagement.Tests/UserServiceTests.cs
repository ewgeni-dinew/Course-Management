namespace CourseManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using CourseManagement.Data;
    using CourseManagement.Data.Factories;
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;
    using CourseManagement.DTO.Account;
    using CourseManagement.Repository;
    using CourseManagement.Repository.Contracts;
    using CourseManagement.Services;
    using CourseManagement.Services.Contracts;

    public class UserServiceTests
    {
        private readonly IUserFactory _userFactory;
        private readonly IRoleFactory _roleFactory;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            this._userFactory = new UserFactory();

            this._roleFactory = new RoleFactory();

            var dbContext = SetupMockDatabaseWithSeedData();

            this._userRepository = new UserRepository(dbContext);

            this._userService = new UserService(this._userRepository, this._userFactory);
        }

        [Fact]
        public void LoginUser_WithValidInput()
        {
            var dto = new LoginUserDTO
            {
                Username = "username@test.com",
                Password = "password"
            };

            var res = this._userService.LoginUser(dto).Result;

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

            var res = this._userService.RegisterUser(dto).Result;

            Assert.Equal(dto.Username, res.Username);
            Assert.Equal(dto.FirstName, res.FirstName);
            Assert.Equal(dto.LastName, res.LastName);
        }

        //register user with already taken username
        //register user with invalid input

        [Fact]
        public void GetAllUsers_WithValidInput()
        {
            var res = this._userService.GetAllUsers().Result;

            Assert.NotEqual(0, res.Count);
        }

        [Fact]
        public void UpdateUser_WithValidInput()
        {
            var dto = new UpdateUserDTO
            {
                Id = 2,
                FirstName = "Updated_FirstName",
                LastName = "Updated_LastName"
            };

            var res = this._userService.UpdateUser(dto).Result;

            Assert.Equal(dto.FirstName, res.FirstName);
            Assert.Equal(dto.LastName, res.LastName);
        }

        [Fact]
        public void ChangeUserPassword_WithValidInput()
        {
            //change password from 'password' to 'Sb123456'

            var dto = new ChangePasswordDTO
            {
                Id = 4,
                Password = "Sb123456",
            };

            var res = this._userService.ChangePassword(dto).Result;

            Assert.NotEqual(0, res);
        }

        [Fact]
        public void BlockUser_ValidInput()
        {
            var dto = new BaseUserDTO
            {
                Id = 1,
            };

            var res = this._userService.BlockUser(dto).Result;

            Assert.Equal(true.ToString(), res.IsBlocked.ToString());
        }

        [Fact]
        public void UnblockUser_ValidInput()
        {
            var dto = new BaseUserDTO
            {
                Id = 1,
            };

            var res = this._userService.UnblockUser(dto).Result;

            Assert.Equal(false.ToString(), res.IsBlocked.ToString());
        }

        [Fact]
        public void DeleteUser_WithValidInput()
        {
            var dto = new BaseUserDTO
            {
                Id = 3
            };

            var res = this._userService.DeleteUser(dto).Result;

            Assert.NotEqual(0, res);
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
                this.BuildSingleUser(1, "Firstname", "Lastname", "username@test.com", "password", 1), //used for LoginUser & Block/Unblock test
                this.BuildSingleUser(2, "Firstname", "Lastname", "username_2@test.com", "password", 1), //used for UpdateUser test
                this.BuildSingleUser(3, "Firstname", "Lastname", "username_3@test.com", "password", 1), //used for DeleteUser test
                this.BuildSingleUser(4, "Firstname", "Lastname", "username_4@test.com", "password", 1), //used for ChangePassword test
            };
        }

        private Role BuildSingleUserRole(int id, string name)
        {
            var role = this._roleFactory
                .WithId(id)
                .WithName(name)
                .Build();

            return role;
        }

        private ApplicationUser BuildSingleUser(int id, string firstName, string lastName, string username, string password, int roleId)
        {
            var user = this._userFactory
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

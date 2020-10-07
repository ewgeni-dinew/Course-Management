namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class UserFactory : IUserFactory
    {
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private int roleId;

        public IUserFactory WithFirstName(string firstName)
        {
            this.firstName = firstName;
            return this;
        }

        public IUserFactory WithLastName(string lastName)
        {
            this.lastName = lastName;
            return this;
        }

        public IUserFactory WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public IUserFactory WithRoleId(int roleId)
        {
            this.roleId = roleId;
            return this;
        }

        public IUserFactory WithUsername(string username)
        {
            this.username = username;
            return this;
        }

        public ApplicationUser Build()
        {
            return new ApplicationUser(this.username, this.password, this.firstName, this.lastName, this.roleId);
        }
    }
}

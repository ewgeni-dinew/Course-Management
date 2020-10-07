namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class RoleFactory : IRoleFactory
    {
        private string name;

        public IRoleFactory WithName(string name)
        {
            this.name = name;

            return this;
        }

        public Role Build()
        {
            return new Role(this.name);
        }
    }
}

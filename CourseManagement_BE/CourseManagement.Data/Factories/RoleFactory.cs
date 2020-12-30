namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class RoleFactory : IRoleFactory
    {
        private int id;
        private string name;

        public IRoleFactory WithId(int id)
        {
            this.id = id;
            return this;
        }

        public IRoleFactory WithName(string name)
        {
            this.name = name;

            return this;
        }

        public Role Build()
        {
            return new Role(this.id, this.name);
        }
    }
}

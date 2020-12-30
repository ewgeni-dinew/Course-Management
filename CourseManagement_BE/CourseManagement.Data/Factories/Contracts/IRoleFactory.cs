namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;

    public interface IRoleFactory : IFactory<Role>
    {
        IRoleFactory WithName(string name);

        IRoleFactory WithId(int id);
    }
}

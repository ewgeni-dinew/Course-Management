namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;

    public interface IUserFactory: IFactory<ApplicationUser>
    {
        IUserFactory WithUsername(string username);

        IUserFactory WithPassword(string password);

        IUserFactory WithFirstName(string firstName);

        IUserFactory WithLastName(string lastName);

        IUserFactory WithRoleId(int roleId);
    }
}

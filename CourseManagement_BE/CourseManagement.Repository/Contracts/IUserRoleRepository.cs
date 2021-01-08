namespace CourseManagement.Repository.Contracts
{
    using CourseManagement.Data.Models;
    using System.Linq;

    public interface IUserRoleRepository
    {
        public IQueryable<Role> GetAllRoles();
    }
}

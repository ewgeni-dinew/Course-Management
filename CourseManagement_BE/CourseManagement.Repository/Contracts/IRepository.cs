namespace CourseManagement.Repository.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    internal interface IRepository<Entity>
    {
        public IQueryable<Entity> GetAll();

        public Task<Entity> GetById(int id);

        public void Delete(Entity entity);

        public void Create(Entity entity);

        public void Update(Entity entity);
    }
}

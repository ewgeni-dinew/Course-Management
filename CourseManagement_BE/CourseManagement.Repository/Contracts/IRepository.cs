namespace CourseManagement.Repository.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<Entity> : ISaveble
    {
        public IQueryable<Entity> GetAll { get; }

        public Task<Entity> GetById(int id);

        public void Delete(Entity entity);

        public void Create(Entity entity);

        public void Update(Entity entity);
    }
}

namespace CourseManagement.Repository.Contracts
{
    using System.Threading.Tasks;
 
    public interface ISaveble
    {
        public Task<int> SaveAsync();

        public int Save();
    }
}

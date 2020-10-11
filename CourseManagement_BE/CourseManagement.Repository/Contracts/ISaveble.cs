namespace CourseManagement.Repository.Contracts
{
    using System.Threading.Tasks;
 
    internal interface ISaveble
    {
        public Task<int> SaveAsync();

        public int Save();
    }
}

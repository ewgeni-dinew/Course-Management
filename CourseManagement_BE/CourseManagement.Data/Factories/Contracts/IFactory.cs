namespace CourseManagement.Data.Factories.Contracts
{
    public interface IFactory<TEntity>
    //where TEntity: <something> if needed
    {
        TEntity Build();
    }
}

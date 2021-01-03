namespace CourseManagement.Data.Factories.Contracts
{
    using CourseManagement.Data.Models;

    public interface ICourseFactory : IFactory<Course>
    {
        ICourseFactory WithId(int id);

        ICourseFactory WithTitle(string title);
        
        ICourseFactory WithSummary(string summary);

        ICourseFactory WithContent(string content);
        
        ICourseFactory WithAuthorId(int authorId);
    }
}

namespace CourseManagement.Data.Models
{
    using CourseManagement.Data.Models.Contracts;

    public class Role : IIdentifiable
    {
        internal Role(string name)
        {
            this.Name = name;
        }

        internal Role(int id, string name)
            : this(name)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}

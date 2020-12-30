namespace CourseManagement.Data.Models
{
    public class Role
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

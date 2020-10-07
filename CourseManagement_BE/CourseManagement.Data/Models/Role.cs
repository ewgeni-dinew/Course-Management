namespace CourseManagement.Data.Models
{
    public class Role
    {
        internal Role(string name)
        {
            this.Name = name;
        }

        internal Role(string name, int id)
            : this(name)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}

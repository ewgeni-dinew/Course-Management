namespace CourseManagement.Data.Models
{
    using System.Collections.Generic;

    public class ApplicationUser
    {
        internal ApplicationUser()
        {
            this.IsBlocked = false;

            this.Courses = new List<Course>();

            this.Favorites = new List<FavoriteCourse>();
        }

        internal ApplicationUser(string username, string password, string firstName, string lastName, int roleId)
            : this()
        {
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RoleId = roleId;
        }

        internal ApplicationUser(int id, string username, string password, string firstName, string lastName, int roleId)
            : this(username, password, firstName, lastName, roleId)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public bool IsBlocked { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int RoleId { get; private set; }

        public Role Role { get; private set; }

        public string Token { get; private set; }

        public ICollection<Course> Courses { get; private set; }

        public ICollection<FavoriteCourse> Favorites { get; private set; }

        //METHODS

        public void UpdateToken(string token)
        {
            this.Token = token;
        }
    }
}

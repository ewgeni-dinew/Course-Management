namespace CourseManagement.Data.Models
{
    using System.Collections.Generic;

    public class ApplicationUser
    {
        public ApplicationUser()
        {
            this.IsBlocked = false;
            
            this.Courses = new List<Course>();

            this.Favorites = new List<FavoriteCourse>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public bool IsBlocked { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<FavoriteCourse> Favorites { get; set; }
    }
}

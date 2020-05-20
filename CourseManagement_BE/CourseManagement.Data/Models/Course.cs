namespace CourseManagement.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Course
    {
        public Course()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser Author { get; set; }

        public int AuthorId { get; set; }

        //Rating

        public ICollection<FavoriteCourse> Favorites { get; set; }
    }
}

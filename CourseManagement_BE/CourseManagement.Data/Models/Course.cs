namespace CourseManagement.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        internal Course()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        internal Course(string title, string summary, string content, int authorId)
            : this()
        {
            this.Title = title;
            this.Summary = summary;
            this.Content = content;
            this.AuthorId = authorId;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public ApplicationUser Author { get; private set; }

        public int AuthorId { get; private set; }

        public double Rating { get; private set; }

        public ICollection<FavoriteCourse> Favorites { get; private set; }
    }
}

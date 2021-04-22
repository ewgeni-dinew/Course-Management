namespace CourseManagement.Data.Models
{
    using CourseManagement.Data.Models.Contracts;
    using System;
    using System.Collections.Generic;

    public class Course : IIdentifiable
    {
        internal Course()
        {
        }

        internal Course(string title, string summary, string content, int authorId)
            : this()
        {
            this.Title = title;
            this.Summary = summary;
            this.Content = content;
            this.AuthorId = authorId;
        }

        internal Course(int id, string title, string summary, string content, int authorId)
            : this(title, summary, content, authorId)
        {
            this.Id = id;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public virtual ApplicationUser Author { get; private set; }

        public int AuthorId { get; private set; }

        public double Rating { get; private set; }

        public ICollection<FavoriteCourse> Favorites { get; private set; }

        //METHODS

        public void UpdateRating(double rating)
        {
            this.Rating = rating;
        }

        public void UpdateTitle(string title)
        {
            this.Title = title;
        }

        public void UpdateSummary(string summary)
        {
            this.Summary = summary;
        }

        public void UpdateContent(string content)
        {
            this.Content = content;
        }
    }
}

﻿namespace CourseManagement.Data.Factories
{
    using CourseManagement.Data.Factories.Contracts;
    using CourseManagement.Data.Models;

    public class CourseFactory : ICourseFactory
    {
        private string title;
        private string summary;
        private string content;
        private int authorId;

        public ICourseFactory WithTitle(string title)
        {
            this.title = title;
            return this;
        }

        public ICourseFactory WithSummary(string summary)
        {
            this.summary = summary;
            return this;
        }

        public ICourseFactory WithContent(string content)
        {
            this.content = content;
            return this;
        }

        public ICourseFactory WithAuthorId(int authorId)
        {
            this.authorId = authorId;
            return this;
        }

        public Course Build()
        {
            return new Course(this.title, this.summary, this.content, this.authorId);
        }
    }
}

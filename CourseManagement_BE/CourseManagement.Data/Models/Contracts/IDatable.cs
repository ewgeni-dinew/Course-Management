namespace CourseManagement.Data.Models.Contracts
{
    using System;

    public interface IDatable
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}

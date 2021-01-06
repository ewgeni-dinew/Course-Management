namespace CourseManagement.Utilities.Errors.Attributes
{
    using System;

    public class ErrorMessageAttribute : Attribute
    {
        public string Message { get; set; }

        public ErrorMessageAttribute(string message)
        {
            this.Message = message;
        }
    }
}

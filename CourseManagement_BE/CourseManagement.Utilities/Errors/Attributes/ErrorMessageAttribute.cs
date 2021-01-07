namespace CourseManagement.Utilities.Errors.Attributes
{
    using System;

    public class ErrorMessageAttribute : Attribute
    {
        public string Message { get; private set; }

        public ErrorMessageAttribute(string message)
        {
            this.Message = message;
        }
    }
}

namespace CourseManagement.DTO.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (new EmailAddressAttribute().IsValid(value)) //validates the email with the build-in functionality
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            throw new ArgumentException("Test");
        }
    }
}

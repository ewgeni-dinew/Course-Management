using System;
namespace CourseManagement.DTO.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //the password must be at least 8 characters, 1 Upper case, 1 Lower case and 1 number
            if (Regex.IsMatch(value.ToString(), @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\S]{8,}$"))
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

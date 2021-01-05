namespace CourseManagement.DTO.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class NumberRangeAttribute : ValidationAttribute
    {
        private readonly int min;
        private readonly int max;

        public NumberRangeAttribute(int min)
        {
            this.min = min;
            this.max = int.MaxValue;
        }

        public NumberRangeAttribute(int min, int max)
            : this(min)
        {
            this.max = max;
        }

        public override bool IsValid(object value)
        {
            if (!int.TryParse(value.ToString(), out int result)) //input value is not a number
            {
                return false;
            }
            else if (result < this.min || result > this.max) //number is out of predefined range
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            throw new ArgumentException("Test");
        }
    }
}

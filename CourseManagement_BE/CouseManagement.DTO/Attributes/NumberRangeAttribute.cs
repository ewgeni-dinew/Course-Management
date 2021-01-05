namespace CourseManagement.DTO.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class NumberRangeAttribute : ValidationAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;

        public NumberRangeAttribute(int min)
        {
            this.minValue = min;
            this.maxValue = int.MaxValue;
        }

        public NumberRangeAttribute(int min, int max)
            : this(min)
        {
            this.maxValue = max;
        }

        public override bool IsValid(object value)
        {
            if (!int.TryParse(value.ToString(), out int result)) //input value is not a number
            {
                return false;
            }
            else if (result < this.minValue || result > this.maxValue) //number is out of predefined range
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

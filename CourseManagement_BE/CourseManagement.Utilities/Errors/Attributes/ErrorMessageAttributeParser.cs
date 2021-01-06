namespace CourseManagement.Utilities.Errors.Attributes
{
    using System.Linq;

    public static class ErrorMessageAttributeParser
    {
        public static string GetMessage(this ErrorMessages value)
        {
            var attribute = value.GetType()
                                 .GetField(value.ToString())
                                 .GetCustomAttributes(typeof(ErrorMessageAttribute), false)
                                 as ErrorMessageAttribute[];

            if (attribute.Any())
            {
                return attribute.First().Message;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

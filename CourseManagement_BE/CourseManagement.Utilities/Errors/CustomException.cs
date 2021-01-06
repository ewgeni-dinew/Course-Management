namespace CourseManagement.Utilities.Errors
{
    using CourseManagement.Utilities.Errors.Attributes;
    using System;
    using System.Text.Json;

    public class CustomException : Exception
    {
        public int ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public CustomException(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;

            this.ErrorMessage = errorMessage;
        }

        public CustomException(ErrorMessages _enum)
            : this((int)_enum, _enum.GetMessage())
        { }

        public override string ToString()
        {
            var obj = new CustomErrorObject(this.ErrorCode, this.ErrorMessage);

            return JsonSerializer.Serialize(obj);
        }
    }
}

namespace CourseManagement.Utilities.Errors
{
    public class CustomErrorObject
    {
        public int ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public CustomErrorObject(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;

            this.ErrorMessage = errorMessage;
        }
    }
}

namespace CourseManagement.Utilities.Errors
{
    public class CustomErrorObject
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public CustomErrorObject(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;

            this.ErrorMessage = errorMessage;
        }
    }
}

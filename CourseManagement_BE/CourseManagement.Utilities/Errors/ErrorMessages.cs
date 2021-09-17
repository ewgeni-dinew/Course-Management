namespace CourseManagement.Utilities.Errors
{
    using CourseManagement.Utilities.Errors.Attributes;

    public enum ErrorMessages
    {
        [ErrorMessage("Whoops something went wrong")]
        GENERAL_ERROR = 1,

        [ErrorMessage("The provided user credentials are not valid.")]
        INVALID_USER_CREDENTIALS = 2,

        [ErrorMessage("The provided username is already taken.")]
        INVALID_USERNAME = 3,

        [ErrorMessage("The provided data is not valid. Please check the input values.")]
        INVALID_INPUT_DATA = 4,

        [ErrorMessage("The provided email address is not in valid format.")]
        INVALID_EMAIL_FORMAT = 5,

        [ErrorMessage("Invalid input password.")]
        INVALID_PASSWORD_FORMAT = 6,

        [ErrorMessage("Error generating PDF file.")]
        GENERATE_PDF_ERROR = 7,

        [ErrorMessage("Error processing the request. Please check the data.")]
        ERROR_PROCESSING_DATA = 8,

        [ErrorMessage("The provided token is invalid.")]
        INVALID_REFRESH_TOKEN = 9,

        [ErrorMessage("No permissions for the specified action.")]
        NO_PERMISSIONS_FOR_ACTION = 10,
    }
}
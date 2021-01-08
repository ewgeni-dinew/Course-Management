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
    }
}
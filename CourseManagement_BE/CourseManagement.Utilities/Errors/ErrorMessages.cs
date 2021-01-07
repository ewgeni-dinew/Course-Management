﻿namespace CourseManagement.Utilities.Errors
{
    using CourseManagement.Utilities.Errors.Attributes;

    public enum ErrorMessages
    {
        [ErrorMessage("Whoops something went wrong")]
        GENERAL_ERROR = 1,

        [ErrorMessage("Testing the second error message")]
        TEST_ERROR = 2,

        [ErrorMessage("Block user error message")]
        BLOCK_USER_ERROR = 3,
    }
}
﻿namespace CourseManagement.DTO.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CourseManagement.Utilities.Errors;

    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullOrEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            throw new CustomException(ErrorMessages.INVALID_INPUT_DATA);
        }
    }
}

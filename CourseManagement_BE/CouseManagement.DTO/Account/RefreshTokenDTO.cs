﻿namespace CourseManagement.DTO.Account
{
    public class RefreshTokenDTO
    {
        public int UserId { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }
    }
}

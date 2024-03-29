﻿namespace CourseManagement.DTO.Account
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string AccessToken { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsDeleted { get; set; }

        public string RefreshToken { get; set; }

        public decimal? GeoLng { get; set; }

        public decimal? GeoLat { get; set; }
    }
}

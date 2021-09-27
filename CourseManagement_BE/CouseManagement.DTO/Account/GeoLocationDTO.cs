namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class GeoLocationDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int UserId { get; set; }

        [NotNullOrEmpty]
        public decimal GeoLng { get; set; }

        [NotNullOrEmpty]
        public decimal GeoLat { get; set; }
    }
}

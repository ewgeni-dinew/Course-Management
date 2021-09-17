namespace CourseManagement.DTO.Account
{
    using CourseManagement.DTO.Attributes;

    public class GeoLocationDTO
    {
        [NotNullOrEmpty]
        [NumberRange(1)]
        public int UserId { get; set; }

        [NotNullOrEmpty]
        public decimal Lng { get; set; }

        [NotNullOrEmpty]
        public decimal Lat { get; set; }
    }
}

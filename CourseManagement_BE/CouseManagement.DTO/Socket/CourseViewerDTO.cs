namespace CourseManagement.DTO.Socket
{
    public class CourseViewerDTO
    {
        public CourseViewerDTO(int courseId, string name)
        {
            this.CourseId = courseId;
            this.Viewer = name;
        }

        public int CourseId { get; set; }

        public string Viewer { get; set; }
    }
}

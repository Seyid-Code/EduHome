namespace EduHome.DTOs.CourseFeature
{
    public class CourseFeatureGetDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool isDeleted { get; set; }

        public Guid CourseID { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}

using website.Models.BaseModels;

namespace EduHome.Models
{
    public class CourseFeature : BaseEntity
    {
        public Course Course { get; set; }
        public Guid CourseID { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
    }
}

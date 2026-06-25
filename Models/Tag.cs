using website.Models.BaseModels;

namespace EduHome.Models
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public List<Course> Courses { get; set; } = new();
        public List<Blog> Blogs { get; set; } = new();
    }
}

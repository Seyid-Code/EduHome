using EduHome.Models;
using website.Models.BaseModels;

namespace website.Models
{
    public class Category : BaseEntity
    {
        public List<Course> Courses { get; set; } = new();
        public List<Blog> Blogs { get; set; } = new();
        public string CategoryName { get; set; }
    }
}

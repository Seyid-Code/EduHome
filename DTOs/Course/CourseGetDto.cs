using EduHome.Models;

namespace EduHome.DTOs.Course
{
    public class CourseGetDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool isDeleted { get; set; }

        public string Image { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryID { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}

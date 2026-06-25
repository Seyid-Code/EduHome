namespace EduHome.DTOs.Course
{
    public class CourseUpdateDto
    {
        public IFormFile? Image { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? Info { get; set; }
        public decimal? Price { get; set; }
    }
}

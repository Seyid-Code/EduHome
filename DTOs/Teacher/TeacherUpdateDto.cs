namespace EduHome.DTOs.Teacher
{
    public class TeacherUpdateDto
    {
        public IFormFile? Image { get; set; }
        public string? Name { get; set; }
        public string? EduLevel { get; set; }
        public string? Desc { get; set; }
        public string? Degree { get; set; }
        public int? Experience { get; set; }
        public string? Faculty { get; set; }
    }
}

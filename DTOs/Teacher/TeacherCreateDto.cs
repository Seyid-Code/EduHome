namespace EduHome.DTOs.Teacher
{
    public class TeacherCreateDto
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string EduLevel { get; set; }
        public string Desc { get; set; }
        public string Degree { get; set; }
        public string Faculty { get; set; }
        public int Experience { get; set; }
    }
}

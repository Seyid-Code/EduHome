namespace EduHome.DTOs.Teacher
{
    public class TeacherGetDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool isDeleted { get; set; }

        public string Image { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public string EduLevel { get; set; }
        public string Desc { get; set; }
        public string Degree { get; set; }
        public int Experience { get; set; }
        public string Faculty { get; set; }
    }
}

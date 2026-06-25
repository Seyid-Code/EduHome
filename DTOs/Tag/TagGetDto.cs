namespace EduHome.DTOs.Tag
{
    public class TagGetDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string TagName { get; set; }
    }
}

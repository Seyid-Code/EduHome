namespace EduHome.DTOs.Category
{
    public class CategoryGetDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string CategoryName { get; set; }
    }
}

using website.Models;
using website.Models.BaseModels;

namespace EduHome.Models
{
    public class Course : BaseEntity
    {
        public string Image {  get; set; }
        public string ImageURL {  get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Tag> Tags { get; set; } = new();

    }
}

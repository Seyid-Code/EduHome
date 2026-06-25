using website.Models.BaseModels;

namespace EduHome.Models
{
    public class Slider : BaseEntity
    {
        public string Image { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}

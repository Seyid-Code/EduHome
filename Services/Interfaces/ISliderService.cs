using EduHome.DTOs.Course;
using EduHome.DTOs.Slider;

namespace EduHome.Services.Interfaces
{
    public interface ISliderService
    {
        Task<bool> CreateAsync(SliderCreateDto dto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<SliderGetDto>> GetAllAsync();
        Task<SliderGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, SliderUpdateDto dto);
    }
}

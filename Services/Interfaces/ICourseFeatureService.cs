using EduHome.DTOs.Category;
using EduHome.DTOs.CourseFeature;
using website.DTOs.Category;

namespace EduHome.Services.Interfaces
{
    public interface ICourseFeatureService
    {
        Task<bool> CreateAsync(CourseFeatureCreateDto dto);
        Task<List<CourseFeatureGetDto>> GetAllAsync();
        Task<bool> RemoveAsync(Guid id);
        Task<CourseFeatureGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, CourseFeatureUpdateDto dto);
    }
}

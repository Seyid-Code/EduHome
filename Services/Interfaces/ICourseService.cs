using EduHome.DTOs.Category;
using EduHome.DTOs.Course;

namespace EduHome.Services.Interfaces
{
    public interface ICourseService
    {
        Task<bool> CreateAsync(CourseCreateDto dto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<CourseGetDto>> GetAllAsync();
        Task<CourseGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, CourseUpdateDto dto);
    }
}

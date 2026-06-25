using EduHome.DTOs.Teacher;

namespace EduHome.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<bool> CreateAsync(TeacherCreateDto dto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<TeacherGetDto>> GetAllAsync();
        Task<TeacherGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, TeacherUpdateDto dto);
    }
}

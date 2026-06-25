using EduHome.DTOs.Tag;

namespace EduHome.Services.Interfaces
{
    public interface ITagService
    {
        Task<bool> CreateAsync(TagCreateDto dto);
        Task<List<TagGetDto>> GetAllAsync();
        Task<bool> RemoveAsync(Guid id);
        Task<TagGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, TagUpdateDto dto);
    }
}

using EduHome.DTOs.Category;
using website.DTOs.Category;

namespace website.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateDto dto);
        Task<List<CategoryGetDto>> GetAllAsync();
        Task<bool> RemoveAsync(Guid id);
        Task<CategoryGetDto> GetAsync(Guid id);
        Task<bool> ToggleAsync(Guid id);
        Task<bool> UpdateAysnc(Guid id, CategoryUpdateDto dto);
    }
}

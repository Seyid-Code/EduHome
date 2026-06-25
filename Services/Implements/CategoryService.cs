using EduHome.DTOs.Category;
using EduHome.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using website.Data;
using website.DTOs.Category;
using website.Models;
using website.Services.Interfaces;

namespace website.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            Category category = new()
            {
                CategoryName = dto.CategoryName,
                CreatedAt = DateTime.Now,
            };
            var result = await _context.AddAsync(category);
            if (result.State != EntityState.Added) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }
        public async Task<bool> RemoveAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return false;

            var result = _context.Remove(category);

            if (result.State != EntityState.Deleted) return false;

            var saveCount = await _context.SaveChangesAsync();

            return saveCount > 0;
        }
        public async Task<List<CategoryGetDto>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            var dtos = categories.Select(c=> new CategoryGetDto()
            {
                CategoryName = c.CategoryName,
                CreatedAt= c.CreatedAt,
                DeletedAt= c.DeletedAt,
                ID = c.ID,
                IsDeleted = c.IsDeleted,
                UpdatedAt = c.UpdatedAt,

            }
            ).ToList();
            return dtos;
        }
        public async Task<CategoryGetDto> GetAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category Not Found!");
            }


            var dto = new CategoryGetDto()
            {
                CategoryName = category.CategoryName,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                ID = category.ID,
                IsDeleted = category.IsDeleted,
                UpdatedAt = category.UpdatedAt,

            }; return dto;
        }

        public async Task<bool> ToggleAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return false;

            category.IsDeleted = !category.IsDeleted;
            category.DeletedAt = DateTime.Now;
            var result = _context.Update(category);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;

        }

        public async Task<bool> UpdateAysnc(Guid id, CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null) return false;

            category.CategoryName = dto.CategoryName is not null ? dto.CategoryName : category.CategoryName;

            category.UpdatedAt = DateTime.Now;
            var result = _context.Update(category);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }

    }
}

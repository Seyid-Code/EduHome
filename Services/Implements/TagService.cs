using Azure;
using EduHome.DTOs.Category;
using EduHome.DTOs.Tag;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using website.Data;
using website.DTOs.Category;
using website.Models;

namespace EduHome.Services.Implements
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TagCreateDto dto)
        {
            Tag tag = new()
            {
                TagName = dto.TagName,
                CreatedAt = DateTime.Now,
            };
            var result = await _context.AddAsync(tag);
            if (result.State != EntityState.Added) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }
        public async Task<bool> RemoveAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null) return false;

            var result = _context.Remove(tag);

            if (result.State != EntityState.Deleted) return false;

            var saveCount = await _context.SaveChangesAsync();

            return saveCount > 0;
        }
        public async Task<List<TagGetDto>> GetAllAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            var dtos = tags.Select(c => new TagGetDto()
            {
                TagName = c.TagName,
                CreatedAt = c.CreatedAt,
                DeletedAt = c.DeletedAt,
                ID = c.ID,
                IsDeleted = c.IsDeleted,
                UpdatedAt = c.UpdatedAt,

            }
            ).ToList();
            return dtos;
        }
        public async Task<TagGetDto> GetAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                throw new Exception("Tag Not Found!");
            }


            var dto = new TagGetDto()
            {
                TagName = tag.TagName,
                CreatedAt = tag.CreatedAt,
                DeletedAt = tag.DeletedAt,
                ID = tag.ID,
                IsDeleted = tag.IsDeleted,
                UpdatedAt = tag.UpdatedAt,
                

            }; return dto;
        }

        public async Task<bool> ToggleAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null) return false;

            tag.IsDeleted = !tag.IsDeleted;
            tag.DeletedAt = DateTime.Now;
            var result = _context.Update(tag);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;

        }

        public async Task<bool> UpdateAysnc(Guid id, TagUpdateDto dto)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null) return false;

            tag.TagName = dto.TagName is not null ? dto.TagName : tag.TagName;

            tag.UpdatedAt = DateTime.Now;
            var result = _context.Update(tag);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }

    }
}
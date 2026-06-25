using EduHome.DTOs.CourseFeature;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using website.Data;

namespace EduHome.Services.Implements
{
    public class CourseFeatureService : ICourseFeatureService
    {
        private readonly AppDbContext _context;

        public CourseFeatureService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CourseFeatureCreateDto dto)
        {
            CourseFeature coursefeature = new()
            {
                CourseID = dto.CourseID,
                Property = dto.Property,
                Value = dto.Value,
                CreatedAt = DateTime.Now
            };
            var result = await _context.AddAsync(coursefeature);
            if (result.State != EntityState.Added) return false;
            var savecount = await _context.SaveChangesAsync();
            return savecount > 0;
        }

        public async Task<List<CourseFeatureGetDto>> GetAllAsync()
        {
            var coursefeatures = await _context.CourseFeatures.ToListAsync();
            var dtos = coursefeatures.Select(c => new CourseFeatureGetDto()
            {
                CourseID = c.CourseID,
                CreatedAt = c.CreatedAt,
                DeletedAt = c.DeletedAt,
                ID = c.ID,
                isDeleted = c.IsDeleted,
                Property = c.Property,
                UpdatedAt = c.UpdatedAt,
                Value = c.Value
            }
            ).ToList();
            return dtos;
        }

        public async Task<CourseFeatureGetDto> GetAsync(Guid id)
        {
            var coursefeature = await _context.CourseFeatures.FindAsync(id);
            if (coursefeature == null)
            {
                throw new Exception("Course Feature Not Found!");
            }


            var dto = new CourseFeatureGetDto()
            {
                CourseID = coursefeature.CourseID,
                CreatedAt = coursefeature.CreatedAt,
                DeletedAt = coursefeature.DeletedAt,
                ID = coursefeature.ID,
                isDeleted = coursefeature.IsDeleted,
                Property = coursefeature.Property,
                UpdatedAt = coursefeature.UpdatedAt,
                Value = coursefeature.Value

            }; return dto;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var coursefeature = await _context.CourseFeatures.FindAsync(id);

            if (coursefeature == null) return false;

            var result = _context.Remove(coursefeature);

            if (result.State != EntityState.Deleted) return false;

            var saveCount = await _context.SaveChangesAsync();

            return saveCount > 0;
        }

        public async Task<bool> ToggleAsync(Guid id)
        {
            var coursefeature = await _context.CourseFeatures.FindAsync(id);

            if (coursefeature == null) return false;

            coursefeature.IsDeleted = !coursefeature.IsDeleted;
            coursefeature.DeletedAt = DateTime.Now;
            var result = _context.Update(coursefeature);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;

        }

        public async Task<bool> UpdateAysnc(Guid id, CourseFeatureUpdateDto dto)
        {
            var coursefeature = await _context.CourseFeatures.FindAsync(id);

            if (coursefeature == null) return false;

            coursefeature.Property = dto.Property is not null ? dto.Property : coursefeature.Property;
            coursefeature.Value = dto.Value is not null ? dto.Value : coursefeature.Value;

            coursefeature.UpdatedAt = DateTime.Now;
            var result = _context.Update(coursefeature);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }
    }
}

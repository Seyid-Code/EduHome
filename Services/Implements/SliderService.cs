using EduHome.DTOs.Course;
using EduHome.DTOs.Slider;
using EduHome.Extentions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using website.Data;

namespace EduHome.Services.Implements
{
        public class SliderService : ISliderService
        {
            private readonly AppDbContext _context;
            private readonly IWebHostEnvironment _web;
            private readonly IHttpContextAccessor _accessor;

            public SliderService(AppDbContext context, IWebHostEnvironment web, IHttpContextAccessor accessor)
            {
                _context = context;
                _web = web;
                _accessor = accessor;
            }

            public async Task<bool> CreateAsync(SliderCreateDto dto)
            {
                Slider slider = new()
                {
                    CreatedAt = DateTime.Now,
                    Desc = dto.Desc,
                    Title = dto.Title,
                    Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Slider"),
                };

                slider.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Slider/{slider.Image}";
                var result = await _context.AddAsync(slider);
                if (result.State != EntityState.Added) return false;
                var saveCount = await _context.SaveChangesAsync();
                return saveCount > 0;
            }

            public async Task<List<SliderGetDto>> GetAllAsync()
            {
                var sliders = await _context.Sliders.ToListAsync();
                var dtos = sliders.Select(c => new SliderGetDto()
                {
                    ID = c.ID,
                    Desc = c.Desc,
                    CreatedAt = c.CreatedAt,
                    DeletedAt = c.DeletedAt,
                    ImageURL = c.ImageURL,
                    Image = c.Image,
                    isDeleted = c.IsDeleted,
                    Title = c.Title,
                    UpdatedAt = c.UpdatedAt,
                }
                ).ToList();
                return dtos;
            }

            public async Task<SliderGetDto> GetAsync(Guid id)
            {
                var slider = await _context.Sliders.FindAsync(id);
                if (slider == null)
                {
                    throw new Exception("Slider Not Found!");
                }


                var dto = new SliderGetDto()
                {
                    ID = slider.ID,
                    Desc = slider.Desc,
                    CreatedAt = slider.CreatedAt,
                    DeletedAt = slider.DeletedAt,
                    ImageURL = slider.ImageURL,
                    Image = slider.Image,
                    isDeleted = slider.IsDeleted,
                    Title = slider.Title,
                    UpdatedAt = slider.UpdatedAt,
                }; return dto;
            }

            public async Task<bool> RemoveAsync(Guid id)
            {
                var slider = await _context.Sliders.FindAsync(id);

                if (slider == null) return false;

                slider.Image.DeleteFile(_web.WebRootPath, "Images", "Slider");

                var result = _context.Remove(slider);

                if (result.State != EntityState.Deleted) return false;

                var saveCount = await _context.SaveChangesAsync();

                return saveCount > 0;
            }

            public async Task<bool> ToggleAsync(Guid id)
            {
                var slider = await _context.Sliders.FindAsync(id);

                if (slider == null) return false;

                slider.IsDeleted = !slider.IsDeleted;
                slider.DeletedAt = DateTime.Now;
                var result = _context.Update(slider);
                if (result.State != EntityState.Modified) return false;
                var saveCount = await _context.SaveChangesAsync();
                return saveCount > 0;

            }

            public async Task<bool> UpdateAysnc(Guid id, SliderUpdateDto dto)
            {
                var slider = await _context.Sliders.FindAsync(id);

                if (slider == null) return false;

                if (dto.Image != null)
                {
                    slider.Image.DeleteFile(_web.WebRootPath, "Images", "Slider");
                    slider.Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Slider");
                    slider.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Slider/{slider.Image}";
                }
                slider.Title = dto.Title is not null ? dto.Title : slider.Title;
                slider.Desc = dto.Desc is not null ? dto.Desc : slider.Desc;

                slider.UpdatedAt = DateTime.Now;
                var result = _context.Update(slider);
                if (result.State != EntityState.Modified) return false;
                var saveCount = await _context.SaveChangesAsync();
                return saveCount > 0;
            }
        }
    }


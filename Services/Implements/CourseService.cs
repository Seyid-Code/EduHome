using EduHome.DTOs.Course;
using EduHome.Extentions;
using EduHome.Migrations;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using website.Data;
using static System.Net.Mime.MediaTypeNames;

namespace EduHome.Services.Implements
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web;
        private readonly IHttpContextAccessor _accessor;

        public CourseService(AppDbContext context, IWebHostEnvironment web, IHttpContextAccessor accessor)
        {
            _context = context;
            _web = web;
            _accessor = accessor;
        }

        public async Task<bool> CreateAsync(CourseCreateDto dto)
        {
            var tags = dto.Tags.Select(t => new Tag
            {
                TagName = t,
            }
            ).ToList();
            Course course = new()
            {
                CategoryID = dto.CategoryID,
                CreatedAt = DateTime.Now,
                Desc = dto.Desc,
                Title = dto.Title,
                Price = dto.Price,
                Info = dto.Info,
                Tags = tags,
                Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Course"),
            };

            course.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Course/{course.Image}";
            var result = await _context.AddAsync(course);
            if (result.State != EntityState.Added) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }

        public async Task<List<CourseGetDto>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            var dtos = courses.Select(c => new CourseGetDto()
            {
                ID = c.ID,
                Desc = c.Desc,
                CreatedAt = c.CreatedAt,
                DeletedAt = c.DeletedAt,
                CategoryID = c.CategoryID,
                ImageURL = c.ImageURL,
                Image = c.Image,
                Info = c.Info,
                isDeleted = c.IsDeleted,
                Price = c.Price,
                Title = c.Title,
                UpdatedAt = c.UpdatedAt,
                Tags = c.Tags.Select(t => t.TagName).ToList()
            }
            ).ToList();
            return dtos;
        }

        public async Task<CourseGetDto> GetAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                throw new Exception("Course Not Found!");
            }


            var dto = new CourseGetDto()
            {
                ID = course.ID,
                Desc = course.Desc,
                CreatedAt = course.CreatedAt,
                DeletedAt = course.DeletedAt,
                CategoryID = course.CategoryID,
                ImageURL = course.ImageURL,
                Image = course.Image,
                Info = course.Info,
                isDeleted = course.IsDeleted,
                Price = course.Price,
                Title = course.Title,
                UpdatedAt = course.UpdatedAt,
                Tags = course.Tags.Select(t => t.TagName).ToList()
            }; return dto;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null) return false;

            course.Image.DeleteFile(_web.WebRootPath, "Images", "Course");

            var result = _context.Remove(course);

            if (result.State != EntityState.Deleted) return false;

            var saveCount = await _context.SaveChangesAsync();

            return saveCount > 0;
        }

        public async Task<bool> ToggleAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null) return false;

            course.IsDeleted = !course.IsDeleted;
            course.DeletedAt = DateTime.Now;
            var result = _context.Update(course);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;

        }

        public async Task<bool> UpdateAysnc(Guid id, CourseUpdateDto dto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null) return false;

            if(dto.Image != null)
            { 
                course.Image.DeleteFile(_web.WebRootPath, "Images", "Course");
                course.Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Course");
                course.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Course/{course.Image}";
            }
            course.Title = dto.Title is not null ? dto.Title : course.Title;
            course.Desc = dto.Desc is not null ? dto.Desc : course.Desc;
            course.Info = dto.Info is not null ? dto.Info : course.Info;
            course.Price = dto.Price is not null ? (decimal)dto.Price : course.Price;

            course.UpdatedAt = DateTime.Now;
            var result = _context.Update(course);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }
    }
}

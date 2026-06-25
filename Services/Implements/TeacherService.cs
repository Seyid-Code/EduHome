using EduHome.DTOs.Teacher;
using EduHome.Extentions;
using EduHome.Models;
using EduHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using website.Data;

namespace EduHome.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web;
        private readonly IHttpContextAccessor _accessor;

        public TeacherService(AppDbContext context, IWebHostEnvironment web, IHttpContextAccessor accessor)
        {
            _context = context;
            _web = web;
            _accessor = accessor;
        }

        public async Task<bool> CreateAsync(TeacherCreateDto dto)
        {
            Teacher teacher = new()
            {
                CreatedAt = DateTime.Now,
                Name = dto.Name,
                EduLevel = dto.EduLevel,
                Desc = dto.Desc,
                Degree = dto.Degree,
                Experience = dto.Experience,
                Faculty = dto.Faculty,
                Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Teacher"),
            };

            teacher.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Teacher/{teacher.Image}";
            var result = await _context.AddAsync(teacher);
            if (result.State != EntityState.Added) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }

        public async Task<List<TeacherGetDto>> GetAllAsync()
        {
            var teachers = await _context.Teachers.ToListAsync();
            var dtos = teachers.Select(c => new TeacherGetDto()
            {
                ID = c.ID,
                Desc = c.Desc,
                CreatedAt = c.CreatedAt,
                DeletedAt = c.DeletedAt,
                ImageURL = c.ImageURL,
                Image = c.Image,
                isDeleted = c.IsDeleted,
                UpdatedAt = c.UpdatedAt,
                Degree = c.Degree,
                EduLevel = c.EduLevel,
                Experience = c.Experience,
                Faculty = c.Faculty,
                Name = c.Name
            }
            ).ToList();
            return dtos;
        }

        public async Task<TeacherGetDto> GetAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                throw new Exception("Teacher Not Found!");
            }


            var dto = new TeacherGetDto()
            {
                ID = teacher.ID,
                Desc = teacher.Desc,
                CreatedAt = teacher.CreatedAt,
                DeletedAt = teacher.DeletedAt,
                ImageURL = teacher.ImageURL,
                Image = teacher.Image,
                isDeleted = teacher.IsDeleted,
                UpdatedAt = teacher.UpdatedAt,
                Name = teacher.Name,
                Faculty = teacher.Faculty,
                Experience= teacher.Experience,
                EduLevel= teacher.EduLevel,
                Degree = teacher.Degree
            }; return dto;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null) return false;

            teacher.Image.DeleteFile(_web.WebRootPath, "Images", "Teacher");

            var result = _context.Remove(teacher);

            if (result.State != EntityState.Deleted) return false;

            var saveCount = await _context.SaveChangesAsync();

            return saveCount > 0;
        }

        public async Task<bool> ToggleAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null) return false;

            teacher.IsDeleted = !teacher.IsDeleted;
            teacher.DeletedAt = DateTime.Now;
            var result = _context.Update(teacher);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;

        }

        public async Task<bool> UpdateAysnc(Guid id, TeacherUpdateDto dto)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null) return false;

            if (dto.Image != null)
            {
                teacher.Image.DeleteFile(_web.WebRootPath, "Images", "Teacher");
                teacher.Image = await dto.Image.CreateFileAsync(_web.WebRootPath, "Images", "Teacher");
                teacher.ImageURL = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Images/Teacher/{teacher.Image}";
            }
            teacher.Name = dto.Name is not null ? dto.Name : teacher.Name;
            teacher.Desc = dto.Desc is not null ? dto.Desc : teacher.Desc;
            teacher.EduLevel = dto.EduLevel is not null ? dto.EduLevel : teacher.EduLevel;
            teacher.Degree = dto.Degree is not null ? dto.Degree : teacher.Degree;
            teacher.Faculty = dto.Faculty is not null ? dto.Faculty : teacher.Faculty;
            teacher.Experience = dto.Experience is not null ? (int)dto.Experience : teacher.Experience;

            teacher.UpdatedAt = DateTime.Now;
            var result = _context.Update(teacher);
            if (result.State != EntityState.Modified) return false;
            var saveCount = await _context.SaveChangesAsync();
            return saveCount > 0;
        }
    }
}

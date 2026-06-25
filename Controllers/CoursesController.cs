using EduHome.DTOs.Course;
using EduHome.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using website.DTOs.Category;

namespace EduHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result ? StatusCode(201, "Created Successful") : BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _service.RemoveAsync(id);
            return result ? StatusCode(204) : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return StatusCode(200, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var result = await _service.GetAsync(id);
            return StatusCode(200, result);
        }
    }
}

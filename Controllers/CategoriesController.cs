using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using website.DTOs.Category;
using website.Services.Interfaces;

namespace website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result ? StatusCode(201, "Created Successful") : BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _service.RemoveAsync(id);
            return result ? StatusCode(204, "Deleted Successful") : BadRequest();
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

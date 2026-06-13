using LinkVault.DTOs.Category;
using LinkVault.Models;
using LinkVault.Services.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LinkVault.Controllers
{
    [ApiController]         
    [Route("api/[controller]")]  
    public class CategoryController : ControllerBase  
        
    {
        public readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _service.GetAll();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            var category = await _service.Create(dto);
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDTO dto)
        {
            var category = await _service.Update(id, dto);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return NoContent();
        }
    }
}

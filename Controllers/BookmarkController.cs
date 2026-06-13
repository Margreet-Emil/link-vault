
using LinkVault.DTOs.Bookmark;
using LinkVault.Models;
using LinkVault.Services.Bookmark;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LinkVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Bookmark : ControllerBase

    {
        public readonly IBookmarkService _service;
        public Bookmark(IBookmarkService service)
        {
            _service = service;
        }
      
        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] FilterBookmarkDTO filters)
        {
            var bookmark = await _service.GetAll(filters);
            return Ok(bookmark);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookmark = await _service.GetById(id);
            return Ok(bookmark);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookmarkDTO dto)
        {
            var bookmark = await _service.Create(dto);
            return Ok(bookmark);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookmarkDTO dto)
        {
            var bookmark = await _service.Update(id, dto);
            return Ok(bookmark);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return NoContent();
        }
    }
}


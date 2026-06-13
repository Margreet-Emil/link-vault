using LinkVault.DTOs.Note;
using LinkVault.Models;
using LinkVault.Services.Note;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LinkVault.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase

    {
        public readonly INoteService _service;
        public NoteController(INoteService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FiltersNoteDTO filters)
        {
            var note = await _service.GetAll(filters);
            return Ok(note);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await _service.GetById(id);
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteDTO dto)
        {
            var note = await _service.Create(dto);
            return Created("Note Created", note);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNoteDTO dto)
        {
            var note = await _service.Update(id, dto);
            return Ok(note);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return NoContent();
        }
        [HttpPatch("{id}/pin")]   
        public async Task<IActionResult> TogglePinned(int id)
        {
            var note = await _service.TogglePinned(id);
            return Ok(note); }
    }
    }


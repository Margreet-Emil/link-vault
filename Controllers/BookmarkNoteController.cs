using LinkVault.DTOs.BookmarkNote;
using LinkVault.Services.BookmarkNote;
using Microsoft.AspNetCore.Mvc;

namespace LinkVault.Controllers
{
    [ApiController]
    [Route("api/bookmarks/{bookmarkId}/notes")]
    public class BookmarkNoteController : ControllerBase
    {
        private readonly IBookNoteService _noteService;

        public BookmarkNoteController(IBookNoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int bookmarkId)
        {
            var notes = await _noteService.GetAllByBookmark(bookmarkId);
            return Ok(notes);
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetById(int bookmarkId, int noteId)
        {
            var note = await _noteService.GetById(noteId);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int bookmarkId, [FromBody] CreateeNoteDTO dto)
        {
            var note = await _noteService.Create(bookmarkId, dto);
            return CreatedAtAction(nameof(GetById), new { bookmarkId, noteId = note.Id }, note);
        }

        [HttpDelete("{noteId}")]
        public async Task<IActionResult> Delete(int bookmarkId, int noteId)
        {
            var result = await _noteService.Delete(bookmarkId, noteId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
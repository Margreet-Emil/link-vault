using LinkVault.Data;
using LinkVault.DTOs.BookmarkNote;
using LinkVault.Exceptions;
using LinkVault.Models;         
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Services.BookmarkNote
{
    public class BookNoteService : IBookNoteService
    {
        private readonly AppDbContext _context;

        public BookNoteService(AppDbContext context)
        {
            _context = context;
        }

        private async Task EnsureBookmarkExists(int bookmarkId)
        {
            var exists = await _context.Bookmarks.AnyAsync(b => b.Id == bookmarkId);
            if (!exists)
                throw new NotFoundException("Bookmark", bookmarkId);
        }

        public async Task<List<ResponseNoteDTO>> GetAllByBookmark(int bookmarkId)
        {
            await EnsureBookmarkExists(bookmarkId);

            return await _context.BookmarkNotes
                .Where(n => n.BookmarkId == bookmarkId)
                .Select(n => new ResponseNoteDTO
                {
                    Id = n.Id,
                    Content = n.Content,
                    CreatedAt = n.CreatedAt,
                    BookmarkId = n.BookmarkId
                })
                .ToListAsync();
        }

        public async Task<ResponseNoteDTO> Create(int bookmarkId, CreateeNoteDTO dto)
        {
            await EnsureBookmarkExists(bookmarkId);

            var note = new Models.BookmarkNote   
            {
                Content = dto.Content,
                BookmarkId = bookmarkId,
            };

            _context.BookmarkNotes.Add(note);
            await _context.SaveChangesAsync();

            return new ResponseNoteDTO
            {
                Id = note.Id,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                BookmarkId = note.BookmarkId
            };
        }

        public async Task<bool> Delete(int bookmarkId, int noteId)
        {
            await EnsureBookmarkExists(bookmarkId);

            var note = await _context.BookmarkNotes
                .FirstOrDefaultAsync(n => n.Id == noteId && n.BookmarkId == bookmarkId);

            if (note == null)
                throw new NotFoundException("Note", noteId);

            _context.BookmarkNotes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }

        

        public Task<DTOs.Note.ResponseNoteDTO> Create(int bookmarkId, DTOs.Note.CreateNoteDTO dto)
        {
            throw new NotImplementedException();
        }

      
        public async Task<LinkVault.Models.BookmarkNote?> GetById(int noteId)
        {
            return await _context.BookmarkNotes
                .FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

       

    }
}
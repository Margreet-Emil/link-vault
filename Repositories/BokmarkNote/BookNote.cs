using LinkVault.Data;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Repositories
{
    public class BookmarkNoteRepository : IBookNote
    {
        private readonly AppDbContext _context;

        public BookmarkNoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookmarkNote>> GetAllByBookmark(int bookmarkId)
        {
            return await _context.BookmarkNotes
                .Where(n => n.BookmarkId == bookmarkId)
                .ToListAsync();
        }

        public async Task<BookmarkNote?> GetById(int noteId)
        {
            return await _context.BookmarkNotes
                .FirstOrDefaultAsync(n => n.Id == noteId);
        }

        public void Add(BookmarkNote note)
        {
            _context.BookmarkNotes.Add(note);
        }

        public void Update(BookmarkNote note)
        {
            _context.BookmarkNotes.Update(note);
        }

        public void Delete(int noteId)
        {
            var note = _context.BookmarkNotes.Find(noteId);
            if (note != null)
                _context.BookmarkNotes.Remove(note);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteById(int noteId)
        {
            var note = _context.BookmarkNotes.Find(noteId);
            if (note != null)
                _context.BookmarkNotes.Remove(note);
        }
    }
}
using LinkVault.Data;
using LinkVault.DTOs.Note;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Repositories
{
    public class NoteRepo : INoteRepo
    {
        private readonly AppDbContext _context;

        public NoteRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Note note)
        {
            _context.Notes.Add(note);
        }

        public void Update(Note note)
        {
            _context.Notes.Update(note);
        }

        public void Delete(Note note)
        {
            _context.Notes.Remove(note);
        }

        public async Task<List<Note>> GetAll(FiltersNoteDTO filters) 
        {
            var q = _context.Notes                                     
                .Include(n => n.Category)
                .AsQueryable();

            if (filters.CategoryId.HasValue)
                q = q.Where(n => n.CategoryId == filters.CategoryId.Value);

            if (filters.Pinned.HasValue)
                q = q.Where(n => n.IsPinned == filters.Pinned.Value);

            if (!string.IsNullOrWhiteSpace(filters.SearchWord))
                q = q.Where(n => n.Title.Contains(filters.SearchWord)
                    || n.Content.Contains(filters.SearchWord));

            return await q.ToListAsync();
        }

        public async Task<Note> GetById(int id)  
        {
            return await _context.Notes
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Note> TogglePinned(int id)  
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
                throw new KeyNotFoundException($"Note with id {id} not found");

            note.IsPinned = !note.IsPinned;         
            await _context.SaveChangesAsync();         
            return note;                               
        }
    }
}

using LinkVault.Data;
using LinkVault.DTOs.Note;
using LinkVault.Exceptions;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Services.Note
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _context;

        public NoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseNoteDTO>> GetAll(FiltersNoteDTO filters)
        {
            var q = _context.Notes
                .Include(n => n.Category)
                .AsQueryable();

            if (filters.CategoryId.HasValue)
                q = q.Where(n => n.CategoryId == filters.CategoryId.Value);

            if (filters.Pinned.HasValue)
                q = q.Where(n => n.IsPinned == filters.Pinned.Value);

            if (!string.IsNullOrWhiteSpace(filters.SearchWord))
                q = q.Where(n => n.Title.Contains(filters.SearchWord) || n.Content.Contains(filters.SearchWord));

            var notes = await q.ToListAsync();

            return notes.Select(n => new ResponseNoteDTO
            {
                NoteId = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned,
                CategoryName = n.Category.Name,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        public async Task<ResponseNoteDTO?> GetById(int id)
        {
            var n = await _context.Notes
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (n == null)
                throw new NotFoundException(nameof(Note), id);

            return new ResponseNoteDTO
            {
                NoteId = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned,
                CategoryName = n.Category.Name,
                CreatedAt = n.CreatedAt
            };
        }

        public async Task<ResponseNoteDTO> Create(CreateNoteDTO dto)
        {
            var existCategory = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);

            if (!existCategory)
                throw new NotFoundException(nameof(Category), dto.CategoryId);

            var note = new Models.Note
            {
                Title = dto.Title,
                Content = dto.Content,
                CategoryId = dto.CategoryId,
                IsPinned = dto.IsPinned
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return await GetById(note.Id);
        }

        public async Task<ResponseNoteDTO?> Update(int id, UpdateNoteDTO dto)
        {
            var existedNote = await _context.Notes.FindAsync(id);

            if (existedNote == null)
                throw new NotFoundException(nameof(Note), id);

            var existCategory = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);

            if (!existCategory)
                throw new NotFoundException(nameof(Category), dto.CategoryId);

            existedNote.Title = dto.Title;
            existedNote.Content = dto.Content;
            existedNote.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return await GetById(existedNote.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var existedNote = await _context.Notes.FindAsync(id);

            if (existedNote == null)
                throw new NotFoundException(nameof(Note), id);

            _context.Notes.Remove(existedNote);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ResponseNoteDTO?> TogglePinned(int id)
        {
            var existedNote = await _context.Notes.FindAsync(id);

            if (existedNote == null)
                throw new NotFoundException(nameof(Note), id);

            existedNote.IsPinned = !existedNote.IsPinned;

            await _context.SaveChangesAsync();

            return await GetById(existedNote.Id);
        }
    }
}
using LinkVault.Data;
using LinkVault.DTOs.Bookmark;
using LinkVault.Exceptions;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Services.Bookmark
{
    public class BookmarkService : IBookmarkService
    {
        private readonly AppDbContext _context;

        public BookmarkService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseBookmarkDTO>> GetAll(FilterBookmarkDTO filters)
        {
            var query = _context.Bookmarks
                .Include(b => b.Category)
                .Include(b => b.BookmarkNotes)
                .AsQueryable();

            if (filters.CategoryId.HasValue)
                query = query.Where(b => b.CategoryID == filters.CategoryId.Value);

            if (filters.IsFavorite.HasValue)
                query = query.Where(b => b.IsFavorite == filters.IsFavorite.Value);

            if (filters.IsArchived.HasValue)
                query = query.Where(b => b.IsArchived == filters.IsArchived.Value);

            if (!string.IsNullOrEmpty(filters.SearchTerm))
                query = query.Where(b => b.Title.Contains(filters.SearchTerm) ||
                                         b.Url.Contains(filters.SearchTerm));

            return await query.Select(b => new ResponseBookmarkDTO
            {
                Id = b.Id,
                Title = b.Title,
                Url = b.Url,
                IsFavorite = b.IsFavorite,
                IsArchived = b.IsArchived,
                CategoryName = b.Category.Name,
                NotesCount = b.BookmarkNotes.Count,
                CreatedAt = b.CreatedAt
            }).ToListAsync();
        }

        public async Task<ResponseBookmarkDTO?> GetById(int id)
        {
            var bookmark = await _context.Bookmarks
                .Include(b => b.Category)
                .Include(b => b.BookmarkNotes)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookmark == null) return null;

            return new ResponseBookmarkDTO
            {
                Id = bookmark.Id,
                Title = bookmark.Title,
                Url = bookmark.Url,
                IsFavorite = bookmark.IsFavorite,
                IsArchived = bookmark.IsArchived,
                CategoryName = bookmark.Category.Name,
                NotesCount = bookmark.BookmarkNotes.Count,
                CreatedAt = bookmark.CreatedAt
            };
        }

        public async Task<ResponseBookmarkDTO> Create(CreateBookmarkDTO dto)
        {
            var urlExists = await _context.Bookmarks.AnyAsync(b => b.Url == dto.Url);
            if (urlExists)
                throw new DuplicateException($"Bookmark with URL '{dto.Url}' already exists");

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
                throw new NotFoundException("Category", dto.CategoryId);

            var bookmark = new Models.Bookmark
            {
                Title = dto.Title,
                Url = dto.Url,
                CategoryID = dto.CategoryId,
                IsFavorite = false,
                IsArchived = false,
            };

            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();

            return await GetById(bookmark.Id);
        }

        public async Task<ResponseBookmarkDTO?> Update(int id, UpdateBookmarkDTO dto)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null) return null;

            bookmark.Title = dto.Title;
            await _context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<ResponseBookmarkDTO?> ToggleFavorite(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null) return null;

            bookmark.IsFavorite = !bookmark.IsFavorite;
            await _context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<ResponseBookmarkDTO?> ToggleArchived(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null) return null;

            bookmark.IsArchived = !bookmark.IsArchived;
            await _context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null) return false;

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
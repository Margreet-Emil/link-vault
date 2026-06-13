using LinkVault.Data;

using LinkVault.DTOs.Category;
using LinkVault.Exceptions;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVault.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseCategoryDTO> Create(CreateCategoryDTO dto)
        {
            var exist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

            if (exist)
                throw new DuplicateException($"Category {dto.Name} is Already Exist");

            var category = new Models.Category
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return await GetById(category.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var categroy = await _context.Categories
            .Include(c => c.Bookmark)
            .Include(c => c.Notes)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (categroy is null)
                throw new NotFoundException(nameof(categroy), id);

            if (categroy.Bookmark.Any() || categroy.Notes.Any())
                throw new BadRequestException("cannot delete category  that has bookmark or notes");


            _context.Categories.Remove(categroy);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ResponseCategoryDTO>> GetAll()
        {
            return await _context.Categories
                .Include(c => c.Bookmark)
                .Include(c => c.Notes)
                .OrderBy(c => c.CreatedAt)
                .Select(c => new ResponseCategoryDTO
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    CategoryDescription = c.Description,
                    CreatedAt = c.CreatedAt,
                    BookmarksCount = c.Bookmark.Count,
                    NotesCount = c.Notes.Count,
                }).ToListAsync();
        }

        public async Task<ResponseCategoryDTO> GetById(int id)
        {
            var categroy = await _context.Categories
                .Include(c => c.Bookmark)
                .Include(c => c.Notes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categroy == null)
                throw new NotFoundException(nameof(categroy), id);

            return new ResponseCategoryDTO
            {
                Id = categroy.Id,
                CategoryName = categroy.Name,
                CategoryDescription = categroy.Description,
                CreatedAt = categroy.CreatedAt,
                BookmarksCount = categroy.Bookmark.Count,
                NotesCount = categroy.Notes.Count,
            };
        }

        public async Task<ResponseCategoryDTO> Update(int id, UpdateCategoryDTO dto)
        {
            var categroy = await _context.Categories.FindAsync(id);

            if (categroy == null)
                throw new NotFoundException(nameof(categroy), id);

            var exist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

            if (exist)
                throw new DuplicateException($"Category {dto.Name} is Already Exist");

            categroy.Name = dto.Name;
            categroy.Description = dto.Description;

            await _context.SaveChangesAsync();
            return await GetById(id);

        }
    }
}
using LinkVault.Data;
using LinkVault.DTOs.Bookmark;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkVault.Repositories
{
    public class BookmarkRepo : IBookmarkRepo
    {
        private readonly AppDbContext _context;
        public BookmarkRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<ResponseBookmarkDTO?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<ResponseBookmarkDTO?> ToggleArchived(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBookmarkDTO?> ToggleFavorite(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Bookmark>> GetAll()
        {
            return await _context.Bookmarks.ToListAsync();
        }

        Task<Bookmark> IBookmarkRepo.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        Task<Bookmark> IBookmarkRepo.ToggleArchived(int id)
        {
            throw new NotImplementedException();
        }

        Task<Bookmark> IBookmarkRepo.ToggleFavorite(int id)
        {
            throw new NotImplementedException();
        }
    }
    }


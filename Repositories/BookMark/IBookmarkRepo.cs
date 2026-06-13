using LinkVault.DTOs.Bookmark;
using LinkVault.Models;

namespace LinkVault.Repositories
{
    public interface IBookmarkRepo
    {

        Task<List<Models.Bookmark>> GetAll();
        Task<Models.Bookmark> GetById(int id);
        void Add(Models.Bookmark bookmark);
        void Create(Models.Bookmark bookmark);
        void Update(Models.Bookmark bookmark);
        Task SaveChangesAsync();
        void Delete(int id);




        Task<Models.Bookmark> ToggleArchived(int id);
        Task<Models.Bookmark> ToggleFavorite(int id);
        
    }
}

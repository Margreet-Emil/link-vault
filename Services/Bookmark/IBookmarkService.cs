using LinkVault.DTOs.Bookmark;

namespace LinkVault.Services.Bookmark
{
    public interface IBookmarkService
    {
        Task<List<ResponseBookmarkDTO>> GetAll(FilterBookmarkDTO filters);
        Task<ResponseBookmarkDTO?> GetById(int id);
        Task<ResponseBookmarkDTO> Create(CreateBookmarkDTO dto);
        Task<ResponseBookmarkDTO?> Update(int id, UpdateBookmarkDTO dto);
        Task<ResponseBookmarkDTO?> ToggleArchived(int id);
        Task<ResponseBookmarkDTO?> ToggleFavorite(int id);
        Task<bool> Delete(int id);
    }
}
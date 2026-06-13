
using LinkVault.DTOs.BookmarkNote;

namespace LinkVault.Services.BookmarkNote
{
    public interface IBookNoteService
    {
        
        Task<List<ResponseNoteDTO>> GetAllByBookmark(int bookmarkId);
        Task<ResponseNoteDTO> Create(int bookmarkId, CreateeNoteDTO dto);
        Task<bool> Delete(int bookmarkId, int noteId);
        Task<LinkVault.Models.BookmarkNote?> GetById(int noteId);
        Task SaveChangesAsync();
    }
}
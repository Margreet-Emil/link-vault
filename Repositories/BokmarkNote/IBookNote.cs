using LinkVault.DTOs.BookmarkNote;
using LinkVault.DTOs.Note;
using LinkVault.DTOs.Bookmark;
using LinkVault.Models;
namespace LinkVault.Repositories
{
    public interface IBookNote
    {

        Task<List<Models.BookmarkNote>> GetAllByBookmark(int bookmarkId);
        Task<Models.BookmarkNote> GetById(int noteId);
        void Add(Models.BookmarkNote note);
        void Update(Models.BookmarkNote note);
        void Delete  ( int   BookmarkId);
        void DeleteById(int noteId);

        Task SaveChangesAsync();


    }
}

using LinkVault.Models;
using LinkVault.DTOs.Note;
namespace LinkVault.Repositories
{
    public interface INoteRepo
    {

        Task<List<Models.Note>> GetAll(FiltersNoteDTO filters);

        Task<Models.Note> GetById(int id);

        
        void Add(Models.Note note); 
        void Delete(Models.Note note);  
        Task SaveChangesAsync();
        
            void Update(Models.Note note);
        Task<Models.Note> TogglePinned(int id);
    }
}

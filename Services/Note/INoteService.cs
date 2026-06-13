
using LinkVault.DTOs.Category;
using LinkVault.DTOs.Note;

namespace LinkVault.Services.Note
{
    public interface INoteService
    {
        Task<List<ResponseNoteDTO>> GetAll(FiltersNoteDTO filters);

        Task<ResponseNoteDTO?> GetById(int id); 

        Task<ResponseNoteDTO> Create(CreateNoteDTO dto);

        Task<ResponseNoteDTO?> Update(int id, UpdateNoteDTO dto);
        
        Task<bool> Delete(int id);

        Task<ResponseNoteDTO?> TogglePinned(int id);
    }
}

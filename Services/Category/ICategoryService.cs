using LinkVault.DTOs.Category;

namespace LinkVault.Services.Category
{
    public interface ICategoryService
    {
        Task<List<DTOs.Category.ResponseCategoryDTO>>GetAll();
        Task<ResponseCategoryDTO> GetById(int id);
        Task<ResponseCategoryDTO> Create(CreateCategoryDTO dto);
        Task<ResponseCategoryDTO> Update( int id ,UpdateCategoryDTO dto);
        Task<bool> Delete(int id);
    }
}

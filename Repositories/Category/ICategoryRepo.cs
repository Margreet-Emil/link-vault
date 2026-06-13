using LinkVault.DTOs.Category;
using LinkVault.Models;


namespace LinkVault.Repositories
{
    public interface ICategoryRepo
    {
        Task<List<Models.Category>> GetAll();
        Task<Models.Category> GetById(int id);
        void Add(Models.Category category);
     void Create(Models.Category category);
        void Update(Models.Category category);
        Task SaveChangesAsync();
         void Delete(int id);
    }
}

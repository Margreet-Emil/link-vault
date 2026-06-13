using LinkVault.Data;
using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkVault.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            throw new NotImplementedException();
        }

        public void Create(Category category)
        {
            throw new NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAll()
        {
           return await _context.Categories.ToListAsync();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }

        
        Task<List<Category>> ICategoryRepo.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Category> ICategoryRepo.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

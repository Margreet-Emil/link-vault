
using LinkVault.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace LinkVault.Repositories.User
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Models.User user)
        {
            await _context.Users.AddAsync(user);
        }

       

        public async Task<List<Models.User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Models.User?> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Models.User user)
        {
            _context.Users.Update(user);
        }

        void IUserRepo.Add(Models.User user)
        {
           _context.Users.AddAsync(user);

        }

        public async void Delete(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                _context.Users.Remove(user);
        }
    }
    }

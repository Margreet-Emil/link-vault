namespace LinkVault.Repositories.User
{
    public interface IUserRepo
    {
        Task<List<Models.User>> GetAll();
        Task<Models.User> GetById(string id);
        void Add(Models.User user);
        void Update(Models.User user);
        void Delete(string  id);
        Task SaveChangeAsync();
    }
}
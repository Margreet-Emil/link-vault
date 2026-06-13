using LinkVault.APIs.DTOs.User;
namespace LinkVault.Services.User
{
    public interface IAuthService
    {
        Task<AuthDto> Register(RegisterDto dto);
        Task<AuthDto> Login(LoginDto dto);
        

        void Delete(string id);
        Task<Models.User> GetById(string id);
    }
}
//using LinkVault.DTOs.User;
//using LinkVault.Exceptions;
//using LinkVault.Models;
//using LinkVault.Repositories.User;
//using LinkVault.Settings;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace LinkVault.Services.User
//{
//    public class AuthService : IAuthService
//    {
//        private readonly UserManager<Models.User> _userManager;
//        private readonly IConfiguration _Config;

//        public AuthService(IConfiguration config)
//        {
//            _Config = config;    
//        }

//        public async Task<ResponseDto> Register(RegisterDto dto)
//       {
//            var userExist = await _userManager.FindByEmailAsync(dto.Email);
//            if (userExist is not null)
//                throw new DuplicateException("Email Already Exist");



//            var user = new Models.User
//            {
//                FirstName = dto.FirstName,
//                LastName = dto.LastName,
//                Email = dto.Email,

//                CreatedAt = DateTime.UtcNow,
//            };

//             await _userManager.CreateAsync(user, dto.Password);

//            await _userManager.AddToRoleAsync(user, "User");

//            return new ResponseDto {
//                Token = await GenerateToken(user)
//            };
//        }

//        public async Task<ResponseDto> Login(LoginDto dto)
//        {
//            var user = await _userManager.FindByEmailAsync(dto.Email);
//            if (user is null)
//                throw new BadHttpRequestException("Invalid Email or pass");

//            var validpass = await _userManager.CheckPasswordAsync(user, dto.Password);
//            if (!validpass)
//                throw new BadHttpRequestException("Invalid Email or pass");

//            return new ResponseDto
//            {
//                Token = await GenerateToken(user)
//            };
//        }

//        private async Task<string> GenerateToken(Models.User user)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:Key"]!));
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id),
//                new Claim(ClaimTypes.Email, user.Email),


//            };

//            var roles = await _userManager.GetRolesAsync(user);
//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }
//        }


//        public void Delete(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Models.User> GetById(string id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
using LinkVault.APIs.DTOs.User;
using LinkVault.Exceptions;
using LinkVault.Models;
using LinkVault.Repositories.User;
using LinkVault.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkVault.Services.User
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Models.User> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<Models.User> userManager, IConfiguration config)
        {
            _userManager = userManager;  
            _config = config;
        }

        public async Task<AuthDto> Register(RegisterDto dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);
            if (userExist is not null)
                throw new DuplicateException("Email Already Exist");

            var user = new Models.User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,      
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new BadHttpRequestException(result.Errors.First().Description);

            await _userManager.AddToRoleAsync(user, "User");

            return new AuthDto
            {
                Token = await GenerateToken(user)
            };
        }

        public async Task<AuthDto> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw new BadHttpRequestException("Invalid Email or Password");

            var validPass = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!validPass)
                throw new BadHttpRequestException("Invalid Email or Password");

            return new AuthDto
            {
                Token = await GenerateToken(user)
            };
        }

        private async Task<string> GenerateToken(Models.User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public async Task<AuthDto> GenerateToken(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw new BadHttpRequestException("User not found");

            return new AuthDto
            {
                Token = await GenerateToken(user)
            };
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.User> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
using LinkVault.APIs.DTOs.User;
using LinkVault.Models ;

using LinkVault.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkVault.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var token = await _service.Register(dto);
            return Ok(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(token);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult me()
        {
            return Ok(new
            {
                id = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                email = User.FindFirst(ClaimTypes.Email).Value,
                role = User.FindFirst("Role").Value
            });
        }
    }
}
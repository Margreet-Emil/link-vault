using System.ComponentModel.DataAnnotations;

namespace LinkVault.APIs.DTOs.User
{
    public class RegisterDto
    {
        [Required, MaxLength(20)]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;

namespace LinkVault.Models
{
    public class User:IdentityUser
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public DateTime CreatedAt { get; set; }
               
       
    }
}
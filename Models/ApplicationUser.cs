using Microsoft.AspNetCore.Identity;
namespace Mini_Cinema.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { set; get; }
        public string? Address { set; get; }
    }
}

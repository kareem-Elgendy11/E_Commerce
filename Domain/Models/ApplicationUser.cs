using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string ImagrUrlProfile { get; set; } = string.Empty;
        public List<Reviews>? Reviews { get; set; }
        public List<Order>? Orders { get; set; }
        public List<RefreshToken> refreshTokens { get; set; } = new List<RefreshToken>();

    }
}

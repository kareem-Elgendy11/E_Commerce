using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Refreshtoken { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn is null && ExpireAt > DateTime.Now;
        public bool IsExpire =>ExpireAt > DateTime.Now;
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = new();

    }
}

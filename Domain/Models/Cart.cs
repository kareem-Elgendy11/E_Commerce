using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User {  get; set; }
        public List<CartItem>? CartItems { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

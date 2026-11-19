using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class CartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("cart")]
        public string CartId { get; set; } = string.Empty;
        public Cart? cart { get; set; }
        [ForeignKey("product")]
        public string ProductId { get; set; } = string.Empty;
        public Product? product { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Should be a positive integer")]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Total Price Should be a positive number")]
        public decimal TotalPrice { get; set; }
    }
}

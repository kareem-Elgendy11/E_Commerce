using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public int Quantity { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; } = string.Empty;
        public Category? Category { get; set; }
        public List<ProductImages>? ProductImages { get; set; }
        public List<Reviews>? Reviews { get; set; } 
        
    }
}

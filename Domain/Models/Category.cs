using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; } = string.Empty;
        public List<Product>? Products { get; set; } 

    }
}

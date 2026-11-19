using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.CartDto
{
    public class AddToCartDto
    {
        //public string Name { get; set; } = string.Empty;
        //public string Image { get; set; } = string.Empty;
        //[Range(1, int.MaxValue, ErrorMessage = "Quantity Should be a positive integer")]
        //public int Quantity { get; set; }
        //[Range(0, double.MaxValue, ErrorMessage = "Total Price Should be a positive number")]
        //public decimal TotalPrice { get; set; }
        ////[Required]
        ////public string CartId { get; set; } = string.Empty;
        //[Required]
        //public string ProductId { get; set; } = string.Empty;
        [Required]
        public string ProductId { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Should be a positive integer")]
        public int Quantity { get; set; }
    }
}

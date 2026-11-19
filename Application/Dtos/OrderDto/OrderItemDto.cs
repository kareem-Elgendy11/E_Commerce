using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.OrderDto
{
    public class OrderItemDto
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } 

       
    }
}

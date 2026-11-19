using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Order")]
        public string OrderId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Order? Order { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public enum OrderStatus
    {
        Pending,
        Cancelled,
        Delivered
    }
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}

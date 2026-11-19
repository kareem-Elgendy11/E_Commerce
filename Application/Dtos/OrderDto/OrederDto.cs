using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.OrderDto
{
    public class OrederDto
    {
        public string Id { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}

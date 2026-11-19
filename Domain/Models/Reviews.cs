using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Reviews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Comment { get; set; } = string.Empty;             
        public int Rating { get; set; } = 0;
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("Product")]
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public ApplicationUser? User { get; set; }

    }
}

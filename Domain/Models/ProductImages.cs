using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class ProductImages
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Image { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
        [ForeignKey("Product")]
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }

    }
}

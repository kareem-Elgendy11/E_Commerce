using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.ProductImageDto
{
    public class UpdateImagedto
    {
        [Required]
        public IFormFile? File { get; set; }
        [Required]
        public string ImageId { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.ProductImageDto
{
    public class AddImagesDto
    {
        public List<IFormFile> file { get; set; } = new List<IFormFile>();
        public string ProductId {  get; set; }= string.Empty;

    }
}

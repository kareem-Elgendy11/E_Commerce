using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.ReviewDto
{
    public class ReviewAllDto
    {
        public string Id { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
    }
}

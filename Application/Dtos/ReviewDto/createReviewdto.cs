using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Dtos.ReviewDto
{
    public class createReviewdto
    {
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
        public string ProductId { get; set; } = string.Empty;
    }
}

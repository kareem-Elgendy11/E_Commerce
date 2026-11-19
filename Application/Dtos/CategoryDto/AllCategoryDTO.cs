using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.CategoryDto
{
    public class AllCategoryDTO
    {
        public string Id { get; set; } 
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; } = string.Empty;
    }
}

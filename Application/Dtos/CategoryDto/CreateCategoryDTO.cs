using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.CategoryDto
{
    public class CreateCategoryDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; } = string.Empty;
    }
}

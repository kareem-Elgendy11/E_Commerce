using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Roles
{
    public class CreateRoleDto
    {
        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Role Name Should be word only ")]
        public string Name { get; set; } = string.Empty;
    }
}

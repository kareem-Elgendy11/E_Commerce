using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Roles
{
    public  class AssignRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}

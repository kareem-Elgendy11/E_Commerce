using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.AuthDto
{
    public class ChangePassowrdDto
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}

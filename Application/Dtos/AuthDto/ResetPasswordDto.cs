using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.AuthDto
{
    public class ResetPasswordDto
    {
        [Required,DataType(DataType.Password)]

        public string NewPassword { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserResDto
    {
        public string ID {  get; set; }=string.Empty;
        public string? UserName { get; set; }
        public string Email {  get; set; }=string.Empty;

    }
}

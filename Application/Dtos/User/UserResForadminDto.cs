using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserResForadminDto
    {
        public string ID { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed {  get; set; }
        public bool LokedOutEnable { get; set; }


    }
}

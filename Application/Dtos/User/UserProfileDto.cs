using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserProfileDto
    {
        public string Id {  get; set; }=string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FIrstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;



    }
}

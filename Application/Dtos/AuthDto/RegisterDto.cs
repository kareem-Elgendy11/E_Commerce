using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(25,ErrorMessage ="UserName is Large")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "UserName is Large")]
        [RegularExpression("^[A-Za-z0-9/-]+$", ErrorMessage = "Don’t Put symbol ")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20 ,ErrorMessage = "UserName is Large")]
        [RegularExpression("^[A-Za-z0-9/-]+$", ErrorMessage = "Don’t Put symbol ")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^[A-Za-z0-9/-]+$",ErrorMessage ="Don’t Put symbol ")]
        public string Address { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }=string.Empty;
        [RegularExpression("^01[0125]{1}[0-9]{8}",ErrorMessage ="Error In Enter Phone")]
        public string Phone {  get; set; }= string.Empty;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        public string ConfirmPassword {  get; set; } = string.Empty;


    }
}

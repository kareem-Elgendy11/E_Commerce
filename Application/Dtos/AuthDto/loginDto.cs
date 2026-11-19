

namespace Application.Dtos.AccountDto
{
    public class loginDto
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9.@_-]+$",ErrorMessage ="Please insert USerName is Valid")]
        public string UserName { get; set; }=string.Empty;
        [DataType(DataType.Password),Required]
        public string Password { get; set; } = string.Empty;
    }
}

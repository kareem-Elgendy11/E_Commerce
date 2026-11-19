namespace Application.Dtos.CartDto
{
    public class UpdateQuantity
    {
        [Required]
        public string ProductId { get; set; } = string.Empty;
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Should be a positive integer")]
        public int Quantity { get; set; }
    }
}

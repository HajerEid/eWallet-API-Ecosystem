using System.ComponentModel.DataAnnotations;

namespace eWallet.Core.Dtos
{
    public class TransactionRequestDto
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }
        public string? Description { get; set; }
    }
}

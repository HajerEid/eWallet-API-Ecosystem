namespace eWallet.Core.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Deposit, Withdraw, Transfer
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}

namespace eWallet.Core.Dtos
{
    public class WalletDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public ICollection<TransactionDto> Transactions { get; set; }
    }
}

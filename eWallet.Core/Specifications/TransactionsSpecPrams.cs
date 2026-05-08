namespace eWallet.Core.Specifications
{
    public class TransactionsSpecPrams
    {
        
        public string? Sort { get; set; }
        public string? Type { get; set; }

        public int? WalletId { get; set; }

        private const int MaxPageSize = 50;
        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize; 

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int PageIndex { get; set; } = 1;

    }

}
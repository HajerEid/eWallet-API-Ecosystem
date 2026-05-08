using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Entities.eWallet
{
    public enum TransactionType
    {
        Deposit,    
        Withdrawal ,
        Transfer
    }

    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public TransactionType Type { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}

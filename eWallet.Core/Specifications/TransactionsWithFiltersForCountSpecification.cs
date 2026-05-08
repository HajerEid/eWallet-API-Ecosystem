using eWallet.Core.Entities.eWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class TransactionsWithFiltersForCountSpecification : BaseSpecification<Transaction>
    {
        public TransactionsWithFiltersForCountSpecification(TransactionsSpecPrams prams)
        : base(t =>
            (t.WalletId == prams.WalletId) &&
            (string.IsNullOrEmpty(prams.Type) || t.Type == Enum.Parse<TransactionType>(prams.Type, true))
        )
        {
            
        }
    }
}

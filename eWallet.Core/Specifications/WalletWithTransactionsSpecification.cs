using eWallet.Core.Entities.eWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class WalletWithTransactionsSpecification : BaseSpecification<Wallet>
    {
        public WalletWithTransactionsSpecification(string userId)
            : base(w => w.AppUserId == userId)
        {
            AddInclude(w => w.Transactions);
        }
    }
}

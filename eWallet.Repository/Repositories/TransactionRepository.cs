using E_Wallet.Repository.Data;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Repository.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction> , ITransactionRepository  
    {
        private readonly eWalletDbContext _context;

        public TransactionRepository(eWalletDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

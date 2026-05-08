using E_Wallet.Repository.Data;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using eWallet.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Repository.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
          private readonly eWalletDbContext _context;
        
        public WalletRepository(eWalletDbContext context) : base(context) {
        
            _context = context;
        }

        

        public async Task<Wallet?> GetByUserIdAsync(string userId)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.AppUserId == userId);
        }


       
    }
}

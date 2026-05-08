using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Repositories
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        //Task<Wallet> GetByReceiverEmailAsync(string receiverEmail);
        Task<Wallet> GetByUserIdAsync(string userId);
    }
}

using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IWalletRepository Wallets { get; }
        ITransactionRepository Transactions { get; }

        IGenericRepository<T> Repository<T>() where T: BaseEntity;

        Task<int> Complete();
    }
}

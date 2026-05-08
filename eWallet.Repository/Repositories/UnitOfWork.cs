using E_Wallet.Repository.Data;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Repo;
using eWallet.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly eWalletDbContext _context;
        public IWalletRepository Wallets { get; private set; }
        public ITransactionRepository Transactions { get; private set; }

        private Hashtable _repositories;
        public UnitOfWork(eWalletDbContext context)
        {
            _context = context;
            Wallets = new WalletRepository(_context);
            Transactions = new TransactionRepository(_context);
        }
        public IGenericRepository<T>? Repository<T>()
                                            where T : BaseEntity
        {
            if (_repositories == null) 
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = 
                    Activator.CreateInstance(repositoryType
                    .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IGenericRepository<T>;
        }


       

        public async Task<int> Complete() => await _context.SaveChangesAsync();

        
        public void Dispose()
        {
            _context.Dispose(); //close connection Database
        }
    }
}

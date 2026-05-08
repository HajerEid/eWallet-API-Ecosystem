using E_Wallet.Repository.Data;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Repo;
using eWallet.Core.Entities.Specifications;
using eWallet.Repository.Specfications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly eWalletDbContext _dbContext;

        public GenericRepository(eWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity) 
            => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            =>  _dbContext.Set<T>().Remove(entity);
        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);


        public async Task<T?> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync()
             => await _dbContext.Set<T>().ToListAsync();




        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecficationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            return ApplySpecification(spec).CountAsync();
        }
    }
}

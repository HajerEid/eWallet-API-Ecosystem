using eWallet.API.Helpers;
using eWallet.Core.Entities.Repo;
using eWallet.Core.Repositories;
using eWallet.Core.Services;
using eWallet.Repository.Repositories;
using eWallet.Service.Services;

namespace eWallet.API.Extensions
{
    public static class ApplicationServicesExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddAutoMapper(typeof(MappingProfiles));


            return services;
        }
    }
}

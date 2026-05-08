using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Services
{
    public interface IWalletService
    {
        Task<WalletDto?> GetWalletByUserIdAsync(string userId);

        Task<TransactionDto?> ExecuteTransactionAsync(string userId, TransactionRequestDto request, TransactionType type);
        Task<ServiceResult> TransferFundsAsync(string userId, TransferRequestDto request);

        Task<decimal> GetBalanceAsync(string userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Services
{
    public interface ITransferService
    {
        Task<bool> TransferMoneyAsync(string senderId, string receiverEmail, decimal amount, string description, int categoryId);

        Task<bool> DepositAsync(string userId, decimal amount);
    }
}

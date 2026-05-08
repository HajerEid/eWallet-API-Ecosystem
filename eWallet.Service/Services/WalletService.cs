using AutoMapper;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using eWallet.Core.Repositories;
using eWallet.Core.Services;
using eWallet.Core.Specifications;
using eWallet.Core.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace eWallet.Service.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public WalletService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Wallet?> GetWalletByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return await _unitOfWork.Wallets.GetByUserIdAsync(user.Id);
        }


        public async Task<TransactionDto?> ExecuteTransactionAsync
            (string userId, TransactionRequestDto request, TransactionType type)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId);
            if (wallet == null) 
                return null;

            if (request.CategoryId <= 0)
            {
                request.CategoryId = Category.DefaultCategoryId; //def> "other"
            }
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId);
            if (category == null) 
                throw new Exception("Category not found");

            // update Amount:
            if (type == TransactionType.Withdrawal && wallet.Balance < request.Amount)
                throw new Exception("Insufficient balance");

            wallet.Balance = (type == TransactionType.Deposit)
                ? wallet.Balance + request.Amount //Deposit
                : wallet.Balance - request.Amount; //withdrawl

            // logs
            var transaction = CreateTransactionObject(
                wallet.Id,
                request.Amount,
                type,
                request.CategoryId,
                category.Name,
                request.Description
            );

            await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

            var result = await _unitOfWork.Complete();

            return result > 0 ? _mapper.Map<TransactionDto>(transaction) : null;
        }

        public async Task<WalletDto?> GetWalletByUserIdAsync(string userId)
        {
            var spec = new WalletWithTransactionsSpecification(userId);

            var wallet = await _unitOfWork.Wallets.GetEntityWithSpecAsync(spec);

            if (wallet == null) return null;

            return _mapper.Map<WalletDto>(wallet);
        }

        public async Task<ServiceResult> TransferFundsAsync
            (string userId, TransferRequestDto request)
        {
            if (request.Amount <= 0)
                return ServiceResult.Failure("Amount must be greater than zero.");

            var senderWallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId);

            var receiverWallet = await GetWalletByEmail(request.ReceiverEmail);
            if (receiverWallet == null) 
                return ServiceResult.Failure("Receiver email not found.");

            if (receiverWallet.AppUserId == userId) 
                return ServiceResult.Failure("You cannot transfer to same wallet.");

            if (senderWallet.Balance < request.Amount) 
                return ServiceResult.Failure("Insufficient balance.");

        

            var category = await _unitOfWork.Repository<Category>()
                            .GetByIdAsync(request.CategoryId);
            if (category == null)
                return ServiceResult.Failure("Invalid Category Selected.");


            senderWallet.Balance -= request.Amount; //wothdrawl from sender
            receiverWallet.Balance += request.Amount; // deposit to reciever

            // logs
            var senderLog = CreateTransactionObject
                (senderWallet.Id,
                request.Amount,
                TransactionType.Transfer,
                request.CategoryId,
                category.Name,
                $"Sent to {request.ReceiverEmail}");
            var receiverLog = CreateTransactionObject(
                receiverWallet.Id,
                request.Amount, 
                TransactionType.Transfer,
                request.CategoryId,
                category.Name,
                $"Received from {senderWallet}");


            await _unitOfWork.Repository<Transaction>().AddAsync(senderLog);
            await _unitOfWork.Repository<Transaction>().AddAsync(receiverLog);

            var result = await _unitOfWork.Complete() > 0; //Row effected?

            return result ? 
                ServiceResult.Success() : ServiceResult.Failure("Save failed.");
        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId);
            return wallet?.Balance ?? 0;
        }



        private Transaction CreateTransactionObject
            (int walletId, decimal amount,
            TransactionType type,
            int categoryId,
            string? categoryName,
            string description )
        {
            return new Transaction
            {
                WalletId = walletId,
                Amount = amount,
                Type = type,
                CategoryId = categoryId,
                Category = new Category { Name = categoryName ?? "Uncategorized" },
                Description = description,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

using AutoMapper;
using eWallet.API.Dtos;
using eWallet.API.Error;
using eWallet.API.Helpers;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using eWallet.Core.Repositories;
using eWallet.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eWallet.API.Controllers
{
   
    public class TransactionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionController(
            IUnitOfWork unitOfWork,
            IMapper mapper
           )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<TransactionDto>>>> GetMyTransactions(
            [FromQuery] TransactionsSpecPrams prams
        )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId);

            if (wallet is null) 
                return NotFound(new ApiResponse(404, "Wallet not found"));

            prams.WalletId = wallet.Id;

            var spec = new TransactionsWithFiltersSpecification(prams);
            var transactions = await _unitOfWork.Transactions.GetAllWithSpecAsync(spec);

            var countSpec = new TransactionsWithFiltersForCountSpecification(prams);
            var totalItems = await _unitOfWork.Transactions.CountAsync(countSpec);

            
            var data = _mapper.Map<IReadOnlyList<Transaction>, IReadOnlyList<TransactionDto>>(transactions);

            return Ok(new Pagination<TransactionDto>
            ( 
                 prams.PageIndex,
                 prams.PageSize,
                 data,
                 totalItems
            ));
        }


    }
}

using AutoMapper;
using eWallet.API.Dtos;
using eWallet.API.Error;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Identity;
using eWallet.Core.Repositories;
using eWallet.Core.Services;
using eWallet.Core.Specifications;
using eWallet.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Security.Claims;

namespace eWallet.API.Controllers
{
    
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<WalletDto>> GetWallet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var walletDto = await _walletService.GetWalletByUserIdAsync(userId);

            if (walletDto == null)
                return NotFound(new ApiResponse(404, "Wallet not found for this user"));

            return Ok(walletDto);
        }

        [Authorize]
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balance = await _walletService.GetBalanceAsync(userId);
            return Ok(new { CurrentBalance = balance });
        }

        [Authorize]
        [HttpPost("deposit")]
        public async Task<ActionResult<TransactionDto>> Deposit(TransactionRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _walletService.ExecuteTransactionAsync(userId, request, TransactionType.Deposit);

            if (result == null) return BadRequest(new ApiResponse(400, "Deposit failed"));

            return Ok(result);
        }


        [Authorize]
        [HttpPost("withdraw")]
        public async Task<ActionResult<TransactionDto>> Withdraw(TransactionRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _walletService.ExecuteTransactionAsync(userId, request, TransactionType.Withdrawal);

            if (result == null) return BadRequest(new ApiResponse(400, "Withdraw failed or insufficient balance"));

            return Ok(result);
        }


        [Authorize]
        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer(TransferRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var result = await _walletService.TransferFundsAsync(userId, request);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(400, result.Message));

            return Ok(
                new { message = "Transfer completed successfully" }
                    );
        }

       
    }
}

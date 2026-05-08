using eWallet.API.Error;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.Identity;
using eWallet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eWallet.API.Controllers
{
    
    public class UserProfileController : BaseApiController
    {
       
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;


        public UserProfileController(UserManager<AppUser> userManager , ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile(UserUpdateDto updateDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return Unauthorized(new ApiResponse(401));


            if (!string.IsNullOrEmpty(updateDto.DisplayName))
                user.DisplayName = updateDto.DisplayName;

            if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
                user.PhoneNumber = updateDto.PhoneNumber;

            if (!string.IsNullOrEmpty(updateDto.Email) && user.Email != updateDto.Email)
            {
                user.Email = updateDto.Email;
                user.UserName = updateDto.Email;
            }


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "Failed to update profile"));

            return Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token =await _tokenService.CreateTokenAsync(user) 
            });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto passwordDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _userManager.ChangePasswordAsync
                (user,
                passwordDto.OldPassword,
                passwordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Password update failed. Please verify the entered information"));
            }

            return Ok("Password updated successfully");
        }


    }

}

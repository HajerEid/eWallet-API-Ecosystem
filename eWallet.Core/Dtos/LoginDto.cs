using System.ComponentModel.DataAnnotations;

namespace eWallet.Core.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

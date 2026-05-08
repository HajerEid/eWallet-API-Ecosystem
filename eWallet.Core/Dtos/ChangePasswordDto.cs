using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace eWallet.Core.Dtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Pssword is Required")]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
            ErrorMessage = "Password must contains 1 Uppercase, 1 Lowercase, 1 Digit, 1 Special Character")]

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("NewPassword", ErrorMessage = "NewPassword and ConfirmNewPassword is not Compare")]
        public string ConfirmNewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Dtos
{
    public class TransferRequestDto : TransactionRequestDto
    {
        [Required(ErrorMessage = "Receiver Email is Required to Transfer Transaction")]
        [EmailAddress(ErrorMessage ="Email Syntax is Not correct")]
        public string ReceiverEmail { get; set; }
    }
}

using eWallet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Entities.eWallet
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; } = 0; 
        public string Currency { get; set; } = "EGP";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string AppUserId { get; set; }

        /// <summary>
        /// public virtual AppUser AppUser { get; set; }
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();



    }
}

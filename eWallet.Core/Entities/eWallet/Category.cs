using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Entities.eWallet
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public TransactionType? TransactionType { get; set; }
        //frontEnd opt
        //public string? Icon { get; set; }
        public string? Description { get; set; }

        public string? AppUserId { get; set; }

        public const int DefaultCategoryId = 1;


        //public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}

using eWallet.Core.Entities.eWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class CategoriesWithUserSpecification : BaseSpecification<Category>
    
    {
        public CategoriesWithUserSpecification(string userId, TransactionType? type)
        : base(x => 
                (x.AppUserId == userId || x.AppUserId == null)
             && (!type.HasValue || x.TransactionType == type || x.TransactionType == null)
        )
        {
            AddOrderByAscending(x => x.Name);
        }
    }
}

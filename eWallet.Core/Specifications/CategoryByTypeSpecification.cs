using eWallet.Core.Entities.eWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class CategoryByTypeSpecification : BaseSpecification<Category>
    {
        public CategoryByTypeSpecification(int id, TransactionType type)
        : base(c => c.Id == id && c.TransactionType == type)
        {
        }
    }
}

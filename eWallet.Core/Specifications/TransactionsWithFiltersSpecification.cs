using eWallet.Core.Entities.eWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class TransactionsWithFiltersSpecification : BaseSpecification<Transaction>
    {
        public TransactionsWithFiltersSpecification(TransactionsSpecPrams prams)
            : base(t => 
                (t.WalletId == prams.WalletId) &&
                (string.IsNullOrEmpty(prams.Type) || t.Type == Enum.Parse<TransactionType>(prams.Type, true))
            )
        {
            if (!string.IsNullOrEmpty(prams.Sort))
            {
                switch (prams.Sort.ToLower())
                {
                    case "dateasc":
                        AddOrderByAscending(p => p.CreatedAt);
                        break;
                    case "datedesc":
                        AddOrderByDescending(p => p.CreatedAt);
                        break;
                    case "amountasc":
                        AddOrderByAscending(p => p.Amount);
                        break;
                    case "amountdesc":
                        AddOrderByDescending(p => p.Amount);
                        break;
                    default:
                        AddOrderByAscending(p => p.CreatedAt);
                        break;
                }
            }else{
                AddOrderByDescending(t => t.CreatedAt);
            }

            AddInclude(x => x.Category);


            ApplyPaging(prams.PageSize * (prams.PageIndex - 1), prams.PageSize);
        }
    }
}

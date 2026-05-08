using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eWallet.Repository.Specfications
{
    public class SpecficationEvaluator <T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            //Sorting:
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            //pagination:
            if (spec.IsPaginationEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }




            query = spec.Includes.Aggregate(query, (current, include) 
                    => current.Include(include));

            return query;
        }
    }
}

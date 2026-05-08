using eWallet.Core.Entities.eWallet;
using eWallet.Core.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get;} = new();


        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnable { get; set; }


        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }


        public BaseSpecification() { } ///No conditions
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void ApplyPaging (int skip, int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
        
        public void AddOrderByAscending(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;

        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDeseExpression)
        {
            OrderByDesc = orderByDeseExpression;

        }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}

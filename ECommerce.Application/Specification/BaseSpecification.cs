using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }


        protected void AddInclude(Expression<Func<TEntity, object>>include)
        {
            IncludeExpressions.Add(include);
        }
        public BaseSpecification(Expression<Func<TEntity, bool>>criteria)
        {
           Criteria = criteria;  
        }


        public Expression<Func<TEntity, object>> OrderBy {  get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending {  get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExp)
        {
            OrderBy=OrderByExp;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByExpDesc)
        {
           OrderByDescending=OrderByExpDesc;
        }


        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagination { get; private set; }


        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPagination = true;
            Take= pageSize;
            Skip=( pageIndex-1)*pageSize;
        }
    }
}

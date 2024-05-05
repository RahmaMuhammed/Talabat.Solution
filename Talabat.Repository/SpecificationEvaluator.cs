using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> Spec)
        {
            var query = inputQuery; // _dpContext.Set<Product>

            if (Spec.Criteria is not null) // p => p.Id == 1
                query = query.Where(Spec.Criteria);

            if (Spec.OrderBy is not null)
                query = query.OrderBy(Spec.OrderBy);
            else if(Spec.OrderByDesc is not null) 
                query = query.OrderByDescending(Spec.OrderByDesc);

            if(Spec.IsPaginationEnable)
                query = query.Skip(Spec.Skip).Take(Spec.Take);

            // query = _dpContext.Set<Product>().Where(p => p.Id == 1)
            // include expression
            // p => p.Brand
            // p => p.Category

            query = Spec.Include.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;

        }
    }
}

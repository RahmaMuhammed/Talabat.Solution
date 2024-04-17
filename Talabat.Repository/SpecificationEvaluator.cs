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

            // query = _dpContext.Set<Product>().Where(p => p.Id == 1)
            // include expression
            // p => p.Brand
            // p => p.Category

            Spec.Include.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}

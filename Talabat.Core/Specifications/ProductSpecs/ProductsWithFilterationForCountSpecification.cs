using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductsWithFilterationForCountSpecification : BaseSpecifications<Product>
    {
        public ProductsWithFilterationForCountSpecification(ProductSpecParams specParams)
              : base(p =>
                       (string.IsNullOrEmpty(specParams.Search) || (p.Name.ToLower().Contains(specParams.Search))) &&
                      (!specParams.brandId.HasValue || p.BrandId == specParams.brandId.Value) &&
                      (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId.Value) 
            )
        { }
    }
}

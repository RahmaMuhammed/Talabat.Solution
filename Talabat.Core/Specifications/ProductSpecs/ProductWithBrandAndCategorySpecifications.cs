using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    internal class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // This Constractor Will Be Used For Creating an Object, That will be used to Get All Products 
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);

        }
    }
}

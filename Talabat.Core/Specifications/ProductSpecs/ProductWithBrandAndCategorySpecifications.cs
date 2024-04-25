using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // This Constractor Will Be Used For Creating an Object, That will be used to Get All Products 
        public ProductWithBrandAndCategorySpecifications(string Sort) : base()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);

            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "priceAsc":
                        //   OrderBy = p => p.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        //  OrderByDesc = p => p.Price;
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                        break;

                }
            }

            else 
                AddOrderBy(p => p.Name);

        }

        // This Constractor Will Be Used For Creating an Object, That will be used to Get a Spacific Product With ID 
        public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude();
        }

        private void AddInclude()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);
        }
    }
}

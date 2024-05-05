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
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base( p =>
                      (!specParams.brandId.HasValue || p.BrandId == specParams.brandId.Value)&&
                      (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId.Value)
            )
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Category);

            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
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

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);

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

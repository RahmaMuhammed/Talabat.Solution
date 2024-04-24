using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; } //Foregin Key Column => ProductBrand
        public string Brand { get; set; } 

        public int CategoryId { get; set; }  //Foregin Key Column => ProductCategory
        public string Category { get; set; } 
    }
}

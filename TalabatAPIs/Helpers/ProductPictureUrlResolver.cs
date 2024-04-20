using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _Configrations;

        public ProductPictureUrlResolver (IConfiguration configuration)
        {
            _Configrations = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
             
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_Configrations["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}

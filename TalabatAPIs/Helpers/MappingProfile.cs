using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
            .ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
            .ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            //.ForMember(P => P.PictureUrl, O => O.MapFrom(S => $"{"https://localhost:7248"}/{S.PictureUrl}"));
        }

        public IConfiguration Configrations { get; }
    }
}

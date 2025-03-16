using AutoMapper;
using DEMO_Product.Domain.Entities;
using DEMO_Product.Shared.DTO;

namespace DEMO_Product.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateOrUpdateProductDto>().ReverseMap();
        }
    }
}

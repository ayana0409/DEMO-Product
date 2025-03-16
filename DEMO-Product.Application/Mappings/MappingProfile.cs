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
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrWhiteSpace(str))
                )); ;
        }
    }
}

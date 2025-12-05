using AutoMapper;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.MappingProfiles
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<AppUserDto, AppUser>().ReverseMap();
            CreateMap<AppUserProfileDto, AppUserProfile>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        }
    }
}

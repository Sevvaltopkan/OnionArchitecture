using AutoMapper;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.CategoryResults;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.ProductResults;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderResults;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.OrderDetailResults;
using OnionVb02.Application.CqrsAndMediatr.CQRS.Results.AppUserProfileResults;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Domain.Entities;

namespace OnionVb02.Application.MappingProfiles
{
    public class ResultMappingProfile : Profile
    {
        public ResultMappingProfile()
        {
            CreateMap<Category, GetCategoryQueryResult>();
            CreateMap<Category, GetCategoryByIdQueryResult>();

            CreateMap<Product, ProductQueryResult>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));

            CreateMap<Order, OrderQueryResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.UserName : null));

            CreateMap<OrderDetail, OrderDetailQueryResult>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product != null ? (decimal?)src.Product.UnitPrice : null));

            CreateMap<AppUserProfile, AppUserProfileQueryResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.UserName : null));

            CreateMap<AppUser, GetAppUserQueryResult>();
            CreateMap<AppUser, GetAppUserByIdQueryResult>();
        }
    }
}

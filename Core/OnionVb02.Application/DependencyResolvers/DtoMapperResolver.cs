using Microsoft.Extensions.DependencyInjection;
using OnionVb02.Application.MappingProfiles;

namespace OnionVb02.Application.DependencyResolvers
{
    public static class DtoMapperResolver
    {
        public static void AddDtoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(DtoMappingProfile),
                typeof(CommandMappingProfile),
                typeof(ResultMappingProfile));
        }
    }
}

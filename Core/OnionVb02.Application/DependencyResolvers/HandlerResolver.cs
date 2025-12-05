using Microsoft.Extensions.DependencyInjection;

namespace OnionVb02.Application.DependencyResolvers
{
    public static class HandlerResolver
    {
        public static void AddHandlerService(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(HandlerResolver).Assembly));
        }
    }
}

using Store.Application;
using Store.Infrastructure;

namespace Store.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI();
            services.AddInfrastructureDI(configuration);
            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Interface;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SqlConnectionFactory>(provider =>
            {
                var connectionString = configuration.GetConnectionString("SqlConStr")!;
                return new SqlConnectionFactory(connectionString);
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

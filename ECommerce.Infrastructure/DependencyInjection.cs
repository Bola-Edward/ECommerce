using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Interceptors;
using ECommerce.Infrastructure.Queries;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Seeding;
using ECommerce.UseCases.Brands;
using ECommerce.UseCases.Products;
using ECommerce.UseCases.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
            );

            services.AddScoped<IDataSeeder, ProductBrandSeeder>();
            services.AddScoped<IDataSeeder, ProductTypeSeeder>();
            services.AddScoped<IAuditInterceptor, AuditInterceptor>();
            services.AddScoped<ISoftDeleteInterceptor, SoftDeleteInterceptor>();


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<DatabaseSeeder>();

            return services;
        }
    }
}
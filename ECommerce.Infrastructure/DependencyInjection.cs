using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
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

            services.AddScoped<DatabaseSeeder>();

            return services;
        }
    }
}
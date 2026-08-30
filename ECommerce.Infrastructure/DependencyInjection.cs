using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Interceptors;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Seeding;
using ECommerce.Infrastructure.Services;
using ECommerce.UseCases.Brands;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Common.Settings;
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

            services.Configure<CloudinarySettings>(options =>
            {
                options.CloudName = configuration["CloudinarySettings:CloudName"]!;
                options.ApiKey = configuration["CloudinarySettings:ApiKey"]!;
                options.ApiSecret = configuration["CloudinarySettings:ApiSecret"]!;
            });

            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<DatabaseSeeder>();

            return services;
        }
    }
}
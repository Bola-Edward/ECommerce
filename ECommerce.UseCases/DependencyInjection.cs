using ECommerce.UseCases.Brands.Queries;
using ECommerce.UseCases.Mapping;
using ECommerce.UseCases.Products.Queries;
using ECommerce.UseCases.Types.Queries;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register use case services here


            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingConfiguration).Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddScoped<GetAllProductsQuery>();
            services.AddScoped<GetProductByIdQuery>();
            services.AddScoped<GetAllBrandsQuery>();
            services.AddScoped<GetAllTypesQuery>();

            return services;
        }
    }
}

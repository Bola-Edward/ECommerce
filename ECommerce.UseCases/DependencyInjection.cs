using ECommerce.UseCases.Brands.Queries;
using ECommerce.UseCases.Mapping;
using ECommerce.UseCases.Messaging;
using ECommerce.UseCases.Products.Queries;
using ECommerce.UseCases.Products.Queries.Validators;
using ECommerce.UseCases.Types.Queries;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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



            services.AddMessaging(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(GetPagedProductQueryValidator).Assembly);

            return services;
        }
    }
}

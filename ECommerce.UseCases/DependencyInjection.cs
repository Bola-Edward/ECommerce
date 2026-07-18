using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register use case services here
            return services;
        }
    }
}

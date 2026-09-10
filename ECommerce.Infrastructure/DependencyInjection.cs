using ECommerce.Domain;
using ECommerce.Domain.IRepositories;
using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Caching;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Interceptors;
using ECommerce.Infrastructure.Identity;
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
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsHistoryTable("__ApplicationMigrationsHistory"))
                .EnableSensitiveDataLogging()
            );

            services.AddDbContext<ECommerceIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsHistoryTable("__IdentityMigrationsHistory"))
                .EnableSensitiveDataLogging()
            );

            services.AddScoped<IDataSeeder, ProductBrandSeeder>();
            services.AddScoped<IDataSeeder, ProductTypeSeeder>();
            services.AddScoped<IDataSeeder, DeliveryMethodSeeder>();
            services.AddScoped<IDataSeeder, IdentitySeeder>();
            services.AddScoped<IAuditInterceptor, AuditInterceptor>();
            services.AddScoped<ISoftDeleteInterceptor, SoftDeleteInterceptor>();

            services.Configure<CloudinarySettings>(options =>
            {
                options.CloudName = configuration["CloudinarySettings:CloudName"]!;
                options.ApiKey = configuration["CloudinarySettings:ApiKey"]!;
                options.ApiSecret = configuration["CloudinarySettings:ApiSecret"]!;
            });

            services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAttachmentService, AttachmentService>();

            services.AddScoped<IEmailVerificationCodeStore, HybridEmailVerificationCodeStore>();
            services.AddScoped<IEmailSender, NoOpEmailSender>();

            services.Configure<EmailVerificationSettings>(
                configuration.GetSection(EmailVerificationSettings.SectionName));

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<DatabaseSeeder>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));


            AddBasketCaching(services, configuration);

            return services;
        }


        private static void AddBasketCaching(IServiceCollection services, IConfiguration config)
        {
            services
                .AddOptions<CacheEntryPolicy>("Basket")
                .Bind(config.GetSection("CachedAggregates:Basket"))
                .ValidateOnStart();

            services.AddSingleton<IValidateOptions<CacheEntryPolicy>, CacheEntryPolicyValidator>();

            var redisConnection = config.GetConnectionString("Redis");

            if (!string.IsNullOrWhiteSpace(redisConnection))
            {
                services.AddStackExchangeRedisCache(options =>
                    options.Configuration = redisConnection);
            }


            services.AddHybridCache();

            services.AddScoped(typeof(ICachedAggregateStore<>), typeof(HybridCacheAggregateStore<>));
            services.AddScoped<IBasketStore, HybridBasketStore>();
        }
    }
}
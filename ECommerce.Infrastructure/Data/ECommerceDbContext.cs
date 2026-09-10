using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly,
                type => type.Namespace == "ECommerce.Infrastructure.Data.Configurations");
        }


        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<ProductBrandEntity> ProductBrands => Set<ProductBrandEntity>();
        public DbSet<ProductTypeEntity> ProductTypes => Set<ProductTypeEntity>();
        public DbSet<UserAddressEntity> UserAddresses => Set<UserAddressEntity>();
        public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();
        public DbSet<DeliveryMethodEntity> DeliveryMethods => Set<DeliveryMethodEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();


    }
}

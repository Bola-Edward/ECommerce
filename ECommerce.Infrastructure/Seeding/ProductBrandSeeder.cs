using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding.Data.Models;


namespace ECommerce.Infrastructure.Seeding
{
    public class ProductBrandSeeder : IDataSeeder
    {
        private readonly ECommerceDbContext _dbContext;


        public ProductBrandSeeder(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int Order => 1;

        public async Task SeedAsync(CancellationToken ct = default)
        {

            await JsonSeeder.SeedIfEmpty<ProductBrandEntity, ProductBrandSeedModel>(
                _dbContext.ProductBrands,
                "brands.json",
                model => ProductBrandEntity.Create(model.Id, model.Name),
                ct
            );
        }
    }
}

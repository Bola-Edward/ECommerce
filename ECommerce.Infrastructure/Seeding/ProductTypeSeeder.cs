using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding.Data.Models;

namespace ECommerce.Infrastructure.Seeding
{
    public class ProductTypeSeeder : IDataSeeder
    {
        private readonly ECommerceDbContext _dbContext;


        public ProductTypeSeeder(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int Order => 2;

        public async Task SeedAsync(CancellationToken ct = default)
        {

            await JsonSeeder.SeedIfEmpty<ProductTypeEntity, ProductTypeSeedModel>(
                _dbContext.ProductTypes,
                "types.json",
                model => ProductTypeEntity.Create(model.Id, model.Name),
                ct
            );
        }
    }
}

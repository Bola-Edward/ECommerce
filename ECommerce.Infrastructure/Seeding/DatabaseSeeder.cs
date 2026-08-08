using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Seeding
{
    public sealed class DatabaseSeeder
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IEnumerable<IDataSeeder> _seeders;


        public DatabaseSeeder(ECommerceDbContext dbContext, IEnumerable<IDataSeeder> seeders)
        {
            _dbContext = dbContext;
            _seeders = seeders;
        }

        public async Task SeedAll(CancellationToken ct = default)
        {

            foreach (var seeder in _seeders.OrderBy(s => s.Order))
            {
                await seeder.SeedAsync(ct);
                await _dbContext.SaveChangesAsync(ct);
            }
        }
    }
}

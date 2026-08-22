using ECommerce.Domain;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Interceptors;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ECommerceDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly IAuditInterceptor _auditInterceptor;
        private readonly ISoftDeleteInterceptor _softDeleteInterceptor;


        public IRepository<ProductEntity> Products { get; }
        public IRepository<ProductBrandEntity> Brands { get; }
        public IRepository<ProductTypeEntity> Types { get; }

        public UnitOfWork(ECommerceDbContext dbContext, IAuditInterceptor auditInterceptor, ISoftDeleteInterceptor softDeleteInterceptor, IRepository<ProductEntity> products, IRepository<ProductBrandEntity> brands, IRepository<ProductTypeEntity> types)
        {
            _dbContext = dbContext;
            _auditInterceptor = auditInterceptor;
            _softDeleteInterceptor = softDeleteInterceptor;

            Products = products;
            Brands = brands;
            Types = types;
        }



        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            _auditInterceptor.Apply(_dbContext);
            _softDeleteInterceptor.Apply(_dbContext);
            return _dbContext.SaveChangesAsync(ct);
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

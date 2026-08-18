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

        public UnitOfWork(ECommerceDbContext dbContext, IAuditInterceptor auditInterceptor, ISoftDeleteInterceptor softDeleteInterceptor)
        {
            _dbContext = dbContext;
            _auditInterceptor = auditInterceptor;
            _softDeleteInterceptor = softDeleteInterceptor;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            if (_repositories.TryGetValue(type, out var repository))
            {
                return (IRepository<T>)repository;
            }

            var newRepository = new Repository<T>(_dbContext);
            _repositories.TryAdd(type, newRepository);
            return newRepository;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            _auditInterceptor.Apply(_dbContext);
            _softDeleteInterceptor.Apply(_dbContext);
            return _dbContext.SaveChangesAsync(ct);
        }
    }
}

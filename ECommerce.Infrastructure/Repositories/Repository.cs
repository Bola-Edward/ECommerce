using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Domain.Repositories;
using ECommerce.Domain.Specification;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public sealed class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #region Read Operations (IReadRepository<T>)

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(ct);
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(ct);
        }

        public async Task<T> SingleAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).SingleAsync(ct);
        }

        public async Task<TResult> SingleAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).SingleAsync(ct);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).ToListAsync(ct);
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken ct = default)
        {
            return await ApplySpecification(specification).ToListAsync(ct);
        }

        public async Task<int> CountAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            return await ApplyCountSpecification(specification).CountAsync(ct);
        }

        public async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            return await ApplyCountSpecification(specification).AnyAsync(ct);
        }

        #endregion

        #region Write Operations (IRepository<T>)

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            entity.MarkAsDeleted();
        }

        #endregion


        public async Task<PagedResult<T>> PagedListAsync(ISpecification<T> specification, CancellationToken ct = default)
        {
            var totalCount = await CountAsync(specification, ct);
            var items = await ListAsync(specification, ct);
            return new PagedResult<T>(items, totalCount);
        }


        #region Helper Methods

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            return SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
        }

        private IQueryable<T> ApplyCountSpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator.GetCountQuery(_dbSet.AsQueryable(), specification);
        }

        #endregion
    }
}
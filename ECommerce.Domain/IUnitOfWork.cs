using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ProductEntity> Products { get; }
        IRepository<ProductBrandEntity> Brands { get; }
        IRepository<ProductTypeEntity> Types { get; }
        IRepository<OrderEntity> Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);

    }
}

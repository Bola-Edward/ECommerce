using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.IRepositories
{
    public interface IBasketStore
    {
        Task<BasketEntity?> GetAsync(Guid buyerId, CancellationToken ct = default);

        Task<BasketEntity> GetOrCreateAsync(Guid buyerId, CancellationToken ct = default);

        Task SaveAsync(BasketEntity basket, CancellationToken ct = default);

        Task DeleteAsync(Guid buyerId, CancellationToken ct = default);
    }
}

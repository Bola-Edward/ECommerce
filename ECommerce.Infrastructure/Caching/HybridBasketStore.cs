using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Caching
{
    public sealed class HybridBasketStore(ICachedAggregateStore<BasketEntity> store) : IBasketStore
    {
        public Task<BasketEntity?> GetAsync(Guid buyerId, CancellationToken ct = default) =>
            store.GetAsync(BuildCacheKey(buyerId), ct);

        public Task<BasketEntity> GetOrCreateAsync(Guid buyerId, CancellationToken ct = default) =>
            store.GetOrCreateAsync(
                BuildCacheKey(buyerId),
                async _ =>
                {
                    var createResult = BasketEntity.CreateEmpty(buyerId);

                    if (createResult.IsFailure)
                        throw new InvalidOperationException(createResult.Error.Message);

                    return createResult.Value;
                },
                ct);

        public Task SaveAsync(BasketEntity basket, CancellationToken ct = default) =>
            store.SetAsync(BuildCacheKey(basket.BuyerId), basket, ct);

        public Task DeleteAsync(Guid buyerId, CancellationToken ct = default) =>
            store.RemoveAsync(BuildCacheKey(buyerId), ct);

        private static string BuildCacheKey(Guid buyerId) => $"basket:{buyerId}";
    }
}

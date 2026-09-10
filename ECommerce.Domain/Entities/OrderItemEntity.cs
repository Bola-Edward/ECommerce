using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class OrderItemEntity
    {
        private OrderItemEntity()
        {
        }

        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public ProductItemOrderedEntity ItemOrdered { get; private set; } = null!;
        public int Quantity { get; private set; }

        public decimal LineTotal => ItemOrdered.UnitPrice * Quantity;

        internal static Result<OrderItemEntity> Create(
            Guid id,
            ProductItemOrderedEntity itemOrdered,
            int quantity)
        {
            if (id == Guid.Empty)
                return Result<OrderItemEntity>.Failure(OrderErrors.InvalidItemId);

            if (itemOrdered is null)
                return Result<OrderItemEntity>.Failure(OrderErrors.InvalidProductId);

            if (quantity < 1)
                return Result<OrderItemEntity>.Failure(OrderErrors.InvalidQuantity);

            return Result<OrderItemEntity>.Success(new OrderItemEntity
            {
                Id = id,
                ItemOrdered = itemOrdered,
                Quantity = quantity
            });
        }

        internal void AssignOrder(Guid orderId) => OrderId = orderId;
    }
}

using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class OrderEntity : BaseEntity
    {
        public const int MaxNameLength = 100;
        public const int MaxPhoneLength = 32;
        public const int MaxCountryLength = 100;
        public const int MaxCityLength = 100;
        public const int MaxStreetLength = 200;
        public const int MaxPostalCodeLength = 20;
        public const int MaxDeliveryMethodNameLength = 100;
        public const int MaxDeliveryTimeLength = 100;

        private readonly List<OrderItemEntity> _items = [];

        private OrderEntity()
        {
        }

        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }

        public Guid DeliveryMethodId { get; private set; }
        public string DeliveryMethodName { get; private set; } = null!;
        public decimal DeliveryMethodPrice { get; private set; }
        public string DeliveryMethodEstimatedTime { get; private set; } = null!;

        public string ShippingRecipientFirstName { get; private set; } = null!;
        public string ShippingRecipientLastName { get; private set; } = null!;
        public string ShippingPhoneNumber { get; private set; } = null!;
        public string ShippingCountry { get; private set; } = null!;
        public string ShippingCity { get; private set; } = null!;
        public string ShippingStreet { get; private set; } = null!;
        public string ShippingPostalCode { get; private set; } = null!;

        public decimal SubTotal { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal Total { get; private set; }

        public IReadOnlyCollection<OrderItemEntity> Items => _items;

        public static Result<OrderEntity> Create(
            Guid id,
            Guid userId,
            DeliveryMethodEntity deliveryMethod,
            UserAddressEntity shippingAddress,
            IReadOnlyList<(Guid ProductId, string ProductName, string PictureUrl, decimal UnitPrice, int Quantity)> basketItems)
        {
            if (id == Guid.Empty)
                return Result<OrderEntity>.Failure(OrderErrors.InvalidId);

            if (userId == Guid.Empty)
                return Result<OrderEntity>.Failure(OrderErrors.InvalidUserId);

            if (deliveryMethod is null)
                return Result<OrderEntity>.Failure(OrderErrors.DeliveryMethodRequired);

            if (!deliveryMethod.IsAvailable)
                return Result<OrderEntity>.Failure(OrderErrors.DeliveryMethodUnavailable);

            if (shippingAddress is null)
                return Result<OrderEntity>.Failure(OrderErrors.ShippingAddressRequired);

            if (shippingAddress.UserId != userId)
                return Result<OrderEntity>.Failure(OrderErrors.ShippingAddressNotOwned);

            if (basketItems is null || basketItems.Count == 0)
                return Result<OrderEntity>.Failure(OrderErrors.EmptyBasket);

            var order = new OrderEntity
            {
                Id = id,
                UserId = userId,
                Status = OrderStatus.Pending,
                DeliveryMethodId = deliveryMethod.Id,
                DeliveryMethodName = deliveryMethod.Name,
                DeliveryMethodPrice = deliveryMethod.Price,
                DeliveryMethodEstimatedTime = deliveryMethod.EstimatedDeliveryTime,
                ShippingRecipientFirstName = shippingAddress.RecipientFirstName,
                ShippingRecipientLastName = shippingAddress.RecipientLastName,
                ShippingPhoneNumber = shippingAddress.PhoneNumber,
                ShippingCountry = shippingAddress.Country,
                ShippingCity = shippingAddress.City,
                ShippingStreet = shippingAddress.Street,
                ShippingPostalCode = shippingAddress.PostalCode,
                ShippingCost = deliveryMethod.Price,
                SubTotal = 0m,
                Total = 0m
            };

            foreach (var line in basketItems)
            {
                var snapshotResult = ProductItemOrderedEntity.Create(
                    line.ProductId,
                    line.ProductName,
                    line.PictureUrl,
                    line.UnitPrice);

                if (snapshotResult.IsFailure)
                    return Result<OrderEntity>.Failure(snapshotResult.Error);

                var itemResult = OrderItemEntity.Create(
                    Guid.NewGuid(),
                    snapshotResult.Value,
                    line.Quantity);

                if (itemResult.IsFailure)
                    return Result<OrderEntity>.Failure(itemResult.Error);

                var item = itemResult.Value;
                item.AssignOrder(id);
                order._items.Add(item);
            }

            order.SubTotal = order._items.Sum(i => i.LineTotal);
            order.Total = order.SubTotal + order.ShippingCost;

            return Result<OrderEntity>.Success(order);
        }

        public Result Cancel()
        {
            if (Status != OrderStatus.Pending)
                return Result.Failure(OrderErrors.CannotCancel);

            Status = OrderStatus.Cancelled;

            return Result.Success();
        }
    }
}

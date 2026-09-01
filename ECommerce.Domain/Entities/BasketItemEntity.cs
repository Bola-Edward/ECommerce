using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Entities
{
    public class BasketItemEntity
    {
        public const int MinQuantity = 1;
        public const int MaxQuantity = 100;


        public Guid Id { get; private set; }
        public string ProductName { get; private set; } = String.Empty;
        public string PictureUrl { get; private set; } = String.Empty;
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        [JsonConstructor]
        private BasketItemEntity(Guid id, string productName, string pictureUrl, decimal unitPrice, int quantity)
        {
            Id = id;
            ProductName = productName;
            PictureUrl = pictureUrl;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public static Result<BasketItemEntity> Create(Guid productId, string productName, string pictureUrl, decimal unitPrice, int quantity)
        {
            if (productId == Guid.Empty)
                return Result<BasketItemEntity>.Failure(BasketErrors.InvalidProductId);

            if (string.IsNullOrWhiteSpace(productName))
                return Result<BasketItemEntity>.Failure(BasketErrors.InvalidProductName);

            if (string.IsNullOrWhiteSpace(pictureUrl))
                return Result<BasketItemEntity>.Failure(BasketErrors.InvalidPictureUrl);

            if (unitPrice <= 0)
                return Result<BasketItemEntity>.Failure(BasketErrors.InvalidUnitPrice);

            if (quantity is < MinQuantity or > MaxQuantity)
                return Result<BasketItemEntity>.Failure(BasketErrors.InvalidQuantity);

            return Result<BasketItemEntity>.Success(
                new BasketItemEntity(
                    productId,
                    productName.Trim(),
                    pictureUrl.Trim(),
                    unitPrice,
                    quantity));
        }


        public decimal LineTotal => UnitPrice * Quantity;

        public Result IncreaseQuantity(int amount)
        {
            if (amount < 0)
            {
                return Result.Failure(BasketErrors.InvalidQuantityIncrement);
            }

            var newQuantity = Quantity + amount;
            if (newQuantity > MaxQuantity)
            {
                return Result.Failure(BasketErrors.QuantityTooHigh);
            }

            Quantity = newQuantity;
            return Result.Success();
        }

        public Result SetQuantity(int quantity)
        {
            if (quantity is < MinQuantity || quantity > MaxQuantity)
            {
                return Result.Failure(BasketErrors.InvalidQuantity);
            }

            Quantity = quantity;
            return Result.Success();
        }

        public Result UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
                return Result.Failure(BasketErrors.InvalidUnitPrice);

            UnitPrice = unitPrice;

            return Result.Success();
        }
    }
}

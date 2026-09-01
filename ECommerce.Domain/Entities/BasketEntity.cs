using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Domain.Entities
{
    public class BasketEntity
    {
        public Guid BuyerId { get; private set; }

        public List<BasketItemEntity> Items { get; private set; } = [];


        [JsonConstructor]
        private BasketEntity(Guid buyerId, List<BasketItemEntity>? items)
        {
            BuyerId = buyerId;
            Items = items ?? [];
        }

        private BasketEntity(Guid buyerId)
        {
            BuyerId = buyerId;
            Items = [];
        }

        public static Result<BasketEntity> CreateEmpty(Guid buyerId)
        {
            if (buyerId == Guid.Empty)
                return Result<BasketEntity>.Failure(BasketErrors.InvalidBuyerId);

            return Result<BasketEntity>.Success(new BasketEntity(buyerId));
        }

        public int TotalItems => Items.Sum(item => item.Quantity);

        public decimal SubTotal => Items.Sum(item => item.LineTotal);



        public Result AddItem(Guid productId, string productName, string pictureUrl,
            decimal unitPrice, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.Id == productId);

            if (existingItem is not null)
                return existingItem.IncreaseQuantity(quantity);


            var createResult = BasketItemEntity.Create(productId, productName, pictureUrl, unitPrice, quantity);

            if (createResult.IsFailure)
                return Result.Failure(createResult.Error);

            Items.Add(createResult.Value);

            return Result.Success();
        }

        public Result RemoveItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.Id == productId);

            if (item is null)
                return Result.Failure(BasketErrors.ItemNotFound);

            Items.Remove(item);

            return Result.Success();
        }

        public Result UpdateItemQuantity(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == productId);

            if (item is null)
                return Result.Failure(BasketErrors.ItemNotFound);


            return item.SetQuantity(quantity);
        }

        public void Clear() => Items.Clear();


        public Result MergeFrom(BasketEntity other)
        {
            if (other.BuyerId == BuyerId)
                return Result.Failure(BasketErrors.CannotMergeSameBuyer);

            foreach (var item in other.Items)
            {
                var mergeResult = AddItem(item.Id, item.ProductName, item.PictureUrl,
                    item.UnitPrice, item.Quantity);

                if (mergeResult.IsFailure)
                    return mergeResult;
            }

            return Result.Success();
        }
    }
}

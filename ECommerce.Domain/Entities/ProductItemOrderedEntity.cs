using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class ProductItemOrderedEntity
    {
        public const int MaxProductNameLength = 200;
        public const int MaxPictureUrlLength = 500;

        private ProductItemOrderedEntity()
        {
        }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = null!;
        public string PictureUrl { get; private set; } = null!;
        public decimal UnitPrice { get; private set; }

        public static Result<ProductItemOrderedEntity> Create(
            Guid productId,
            string productName,
            string pictureUrl,
            decimal unitPrice)
        {
            if (productId == Guid.Empty)
                return Result<ProductItemOrderedEntity>.Failure(OrderErrors.InvalidProductId);

            if (string.IsNullOrWhiteSpace(productName))
                return Result<ProductItemOrderedEntity>.Failure(OrderErrors.InvalidProductName);

            if (string.IsNullOrWhiteSpace(pictureUrl))
                return Result<ProductItemOrderedEntity>.Failure(OrderErrors.InvalidPictureUrl);

            if (unitPrice < 0)
                return Result<ProductItemOrderedEntity>.Failure(OrderErrors.InvalidUnitPrice);

            return Result<ProductItemOrderedEntity>.Success(new ProductItemOrderedEntity
            {
                ProductId = productId,
                ProductName = productName.Trim(),
                PictureUrl = pictureUrl.Trim(),
                UnitPrice = unitPrice
            });
        }
    }
}

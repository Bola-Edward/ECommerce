using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public const int MaxNameLength = 100;
        public const int MaxDescriptionLength = 1000;
        public const int MaxPictureUrlLength = 500;

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        // todo Store the picture on the cloud => Azure Blob Storage, Cloudinary, Cloudflare R2, etc. and store the URL in the database
        public string PictureUrl { get; private set; } = null!;
        public decimal Price { get; private set; }

        // start writing relationship properties
        public Guid ProductBrandId { get; set; }
        public ProductBrandEntity ProductBrand { get; private set; } = null!;

        public Guid ProductTypeId { get; set; }
        public ProductTypeEntity ProductType { get; private set; } = null!;

        private ProductEntity() { }

        public static Result<ProductEntity> Create(
        string name,
        string description,
        string pictureUrl,
        decimal price,
        Guid productBrandId,
        Guid productTypeId)
        {
            var product = new ProductEntity();

            var nameResult = product.SetName(name);
            if (nameResult.IsFailure)
                return Result<ProductEntity>.Failure(nameResult.Error!);

            var descriptionResult = product.SetDescription(description);
            if (descriptionResult.IsFailure)
                return Result<ProductEntity>.Failure(descriptionResult.Error!);

            var pictureUrlResult = product.SetPictureUrl(pictureUrl);
            if (pictureUrlResult.IsFailure)
                return Result<ProductEntity>.Failure(pictureUrlResult.Error!);

            var priceResult = product.SetPrice(price);
            if (priceResult.IsFailure)
                return Result<ProductEntity>.Failure(priceResult.Error!);

            var brandResult = product.SetBrand(productBrandId);
            if (brandResult.IsFailure)
                return Result<ProductEntity>.Failure(brandResult.Error!);

            var typeResult = product.SetType(productTypeId);
            if (typeResult.IsFailure)
                return Result<ProductEntity>.Failure(typeResult.Error!);

            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTimeOffset.UtcNow;

            return Result<ProductEntity>.Success(product);
        }

        public Result Update(
            string name,
            string description,
            string pictureUrl,
            decimal price,
            Guid productBrandId,
            Guid productTypeId)
        {
            var nameResult = SetName(name);
            if (nameResult.IsFailure)
                return nameResult;

            var descriptionResult = SetDescription(description);
            if (descriptionResult.IsFailure)
                return descriptionResult;

            var pictureUrlResult = SetPictureUrl(pictureUrl);
            if (pictureUrlResult.IsFailure)
                return pictureUrlResult;

            var priceResult = SetPrice(price);
            if (priceResult.IsFailure)
                return priceResult;

            var brandResult = SetBrand(productBrandId);
            if (brandResult.IsFailure)
                return brandResult;

            var typeResult = SetType(productTypeId);
            if (typeResult.IsFailure)
                return typeResult;

            return Result.Success();
        }

        private Result SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure(ProductErrors.NameRequired);

            Name = name.Trim();
            return Result.Success();
        }

        private Result SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure(ProductErrors.DescriptionRequired);

            Description = description.Trim();
            return Result.Success();
        }

        private Result SetPictureUrl(string pictureUrl)
        {
            if (string.IsNullOrWhiteSpace(pictureUrl))
                return Result.Failure(ProductErrors.PictureUrlRequired);

            PictureUrl = pictureUrl.Trim();
            return Result.Success();
        }

        private Result SetPrice(decimal price)
        {
            if (price <= 0)
                return Result.Failure(ProductErrors.InvalidPrice);

            Price = price;
            return Result.Success();
        }

        private Result SetBrand(Guid productBrandId)
        {
            if (productBrandId == Guid.Empty)
                return Result.Failure(ProductErrors.ProductBrandRequired);

            ProductBrandId = productBrandId;
            return Result.Success();
        }

        private Result SetType(Guid productTypeId)
        {
            if (productTypeId == Guid.Empty)
                return Result.Failure(ProductErrors.ProductTypeRequired);

            ProductTypeId = productTypeId;
            return Result.Success();
        }
    }
}
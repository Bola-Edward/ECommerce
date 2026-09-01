using ECommerce.Domain.Entities;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Specifications
{
    public sealed class ProductForBasketSpecification : Specification<ProductEntity, ProductForBasketResponse>
    {
        public ProductForBasketSpecification(Guid productId)
        {
            Query
                .Where(product => product.Id == productId)
                .Select(product => new ProductForBasketResponse(
                    product.Id,
                    product.Name,
                    product.PictureUrl,
                    product.Price));
        }
    }
}

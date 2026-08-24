using ECommerce.Domain.Entities;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Specifications
{
    public sealed class ProductByIdSpecification : Specification<ProductEntity, GetProductByIdResponse>
    {
        public ProductByIdSpecification(Guid productId)
        {
            Query
                .Where(product => product.Id == productId)
                .Select(product => new GetProductByIdResponse(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.PictureUrl,
                    product.ProductBrand.Name,
                    product.ProductType.Name));
        }
    }
}

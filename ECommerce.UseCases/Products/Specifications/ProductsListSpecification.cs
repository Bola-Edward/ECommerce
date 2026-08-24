using ECommerce.Domain.Entities;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Specifications
{
    public sealed class ProductsListSpecification : Specification<ProductEntity, GetAllProductsResponse>
    {
        public ProductsListSpecification()
        {
            Query
                .Select(product => new GetAllProductsResponse(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.PictureUrl,
                    product.ProductBrand.Name,
                    product.ProductType.Name))
                .OrderBy(product => product.Name);
        }
    }
}

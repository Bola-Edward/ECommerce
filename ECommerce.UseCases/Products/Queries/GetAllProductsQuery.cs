using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries
{
    public class GetAllProductsQuery
    {
        private readonly IRepository<ProductEntity> _productRepository;

        public GetAllProductsQuery(IRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> ExecuteAsync(CancellationToken ct)
        {
            var products = await _productRepository.ListAsync(new ProductsListSpecification(), ct);
            return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
        }
    }
}

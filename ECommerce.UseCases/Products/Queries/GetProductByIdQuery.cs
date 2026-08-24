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
    public class GetProductByIdQuery
    {
        private readonly IRepository<ProductEntity> _productRepository;

        public GetProductByIdQuery(IRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<GetProductByIdResponse>> ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var product = await _productRepository.FirstOrDefaultAsync(new ProductByIdSpecification(id), ct);

            if (product is null)
            {
                return Result<GetProductByIdResponse>.Failure(ProductErrors.NotFound);
            }

            return Result<GetProductByIdResponse>.Success(product);
        }
    }
}

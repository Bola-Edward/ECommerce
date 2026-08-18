using ECommerce.Domain.Common;
using ECommerce.UseCases.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries
{
    public class GetProductByIdQuery
    {
        private readonly IProductQueryService _productQueryService;

        public GetProductByIdQuery(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        public async Task<Result<GetProductByIdResponse>> ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var product = await _productQueryService.GetProductByIdAsync(id, ct);

            if (product is null)
            {
                return Result<GetProductByIdResponse>.Failure(ProductErrors.NotFound);
            }

            return Result<GetProductByIdResponse>.Success(product);
        }
    }
}

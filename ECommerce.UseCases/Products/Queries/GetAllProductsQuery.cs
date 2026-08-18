using ECommerce.Domain.Common;
using ECommerce.UseCases.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries
{
    public class GetAllProductsQuery
    {
        private readonly IProductQueryService _productQueryService;

        public GetAllProductsQuery(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> ExecuteAsync()
        {
            var products = await _productQueryService.GetAllProductsAsync();
            return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
        }
    }
}

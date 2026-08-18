using ECommerce.Domain.Common;
using ECommerce.UseCases.Brands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands.Queries
{
    public class GetAllBrandsQuery
    {
        private readonly IProductBrandQueryService _productBrandQueryService;

        public GetAllBrandsQuery(IProductBrandQueryService productBrandQueryService)
        {
            _productBrandQueryService = productBrandQueryService;
        }

        public async Task<Result<IReadOnlyList<GetAllBrandsResponse>>> ExecuteAsync(CancellationToken ct = default)
        {
            var brands = await _productBrandQueryService.GetAllBrandsAsync(ct);
            return Result<IReadOnlyList<GetAllBrandsResponse>>.Success(brands);
        }
    }
}

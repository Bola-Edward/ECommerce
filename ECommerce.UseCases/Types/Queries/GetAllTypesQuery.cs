using ECommerce.Domain.Common;
using ECommerce.UseCases.Types.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types.Queries
{
    public class GetAllTypesQuery
    {
        private readonly IProductTypeQueryService _productTypeQueryService;

        public GetAllTypesQuery(IProductTypeQueryService productTypeQueryService)
        {
            _productTypeQueryService = productTypeQueryService;
        }


        public async Task<Result<IReadOnlyList<GetAllTypesResponse>>> ExecuteAsync(CancellationToken ct = default)
        {
            var types = await _productTypeQueryService.GetAllProductTypesAsync(ct);
            return Result<IReadOnlyList<GetAllTypesResponse>>.Success(types);
        }

    }
}

using ECommerce.UseCases.Types.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types
{
    public interface IProductTypeQueryService
    {
        Task<IReadOnlyList<GetAllTypesResponse>> GetAllProductTypesAsync(CancellationToken ct = default);
    }
}

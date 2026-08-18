using ECommerce.UseCases.Brands.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands
{
    public interface IProductBrandQueryService
    {
        Task<IReadOnlyList<GetAllBrandsResponse>> GetAllBrandsAsync(CancellationToken ct = default);
    }
}

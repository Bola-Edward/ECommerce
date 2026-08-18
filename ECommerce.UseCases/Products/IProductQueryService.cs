using ECommerce.UseCases.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products
{
    public interface IProductQueryService
    {
        Task<IReadOnlyList<GetAllProductsResponse>> GetAllProductsAsync(CancellationToken ct = default);

        Task<GetProductByIdResponse?> GetProductByIdAsync(Guid id, CancellationToken ct = default);
    }
}

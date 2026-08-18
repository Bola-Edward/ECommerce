using ECommerce.Infrastructure.Data;
using ECommerce.UseCases.Types;
using ECommerce.UseCases.Types.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Queries
{
    public class ProductTypeQueryService : IProductTypeQueryService
    {
        private readonly ECommerceDbContext _Context;

        public ProductTypeQueryService(ECommerceDbContext context)
        {
            _Context = context;
        }

        public async Task<IReadOnlyList<GetAllTypesResponse>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            return await _Context.ProductTypes
                .ProjectToType<GetAllTypesResponse>()
                .ToListAsync(ct);
        }
    }
}

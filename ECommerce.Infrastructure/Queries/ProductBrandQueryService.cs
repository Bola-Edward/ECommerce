using ECommerce.Infrastructure.Data;
using ECommerce.UseCases.Brands;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Queries
{
    public class ProductBrandQueryService : IProductBrandQueryService
    {
        private readonly ECommerceDbContext _context;
        public ProductBrandQueryService(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<GetAllBrandsResponse>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            return await _context.ProductBrands
                .AsNoTracking()
                .ProjectToType<GetAllBrandsResponse>()
                .ToListAsync(ct);
        }
    }
}

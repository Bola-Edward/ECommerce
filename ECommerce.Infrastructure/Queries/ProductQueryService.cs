using ECommerce.Infrastructure.Data;
using ECommerce.UseCases.Products;
using ECommerce.UseCases.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Infrastructure.Queries
{
    public class ProductQueryService : IProductQueryService
    {

        private readonly ECommerceDbContext _context;
        public ProductQueryService(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<GetAllProductsResponse>> GetAllProductsAsync(CancellationToken ct = default)
        {
            return await _context.Products
                .AsNoTracking()
                .ProjectToType<GetAllProductsResponse>()  // mapseter
                                                          //.Select(p => new GetAllProductsResponse(p.Id, p.Name, p.Description, p.Price, p.PictureUrl, p.ProductType.Name, p.ProductBrand.Name))
                .ToListAsync(ct);

        }

        public async Task<GetProductByIdResponse?> GetProductByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Products
                 .AsNoTracking()
                 .Where(p => p.Id == id)
                 .ProjectToType<GetProductByIdResponse>() // mapseter
                                                          //.Select(p => new GetProductByIdResponse(p.Id, p.Name, p.Description, p.Price, p.PictureUrl, p.ProductType.Name, p.ProductBrand.Name))
                 .FirstOrDefaultAsync(ct);

        }
    }
}

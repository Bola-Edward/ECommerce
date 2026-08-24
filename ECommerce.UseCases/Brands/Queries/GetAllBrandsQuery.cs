using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Brands.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands.Queries
{
    public class GetAllBrandsQuery
    {
        private readonly IRepository<ProductBrandEntity> _productBrandRepository;

        public GetAllBrandsQuery(IRepository<ProductBrandEntity> productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        public async Task<Result<IReadOnlyList<GetAllBrandsResponse>>> ExecuteAsync(CancellationToken ct = default)
        {
            var brands = await _productBrandRepository.ListAsync(new BrandListSpecification(), ct);
            return Result<IReadOnlyList<GetAllBrandsResponse>>.Success(brands);
        }
    }
}

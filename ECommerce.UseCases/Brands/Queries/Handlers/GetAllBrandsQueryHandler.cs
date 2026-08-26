using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Brands.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands.Queries.Handlers
{
    public sealed class GetAllBrandsQueryHandler(IReadRepository<ProductBrandEntity> repository)
    : IRequestHandler<GetAllBrandsQuery, Result<IReadOnlyList<GetAllBrandsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllBrandsResponse>>> Handle(
            GetAllBrandsQuery request,
            CancellationToken cancellationToken)
        {
            var brands = await repository.ListAsync(new BrandListSpecification(), cancellationToken);
            return Result<IReadOnlyList<GetAllBrandsResponse>>.Success(brands);
        }
    }
}

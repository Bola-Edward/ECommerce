using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries.Handlers
{
    public sealed class GetAllProductsQueryHandler(IReadRepository<ProductEntity> repository)
    : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<GetAllProductsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> Handle(
            GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await repository.ListAsync(new ProductsListSpecification(), cancellationToken);
            return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
        }
    }
}

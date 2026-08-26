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
    public sealed class GetProductByIdQueryHandler(IReadRepository<ProductEntity> repository)
    : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        public async Task<Result<GetProductByIdResponse>> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await repository.FirstOrDefaultAsync(
                new ProductByIdSpecification(request.Id),
                cancellationToken);

            if (product is null)
                return Result<GetProductByIdResponse>.Failure(ProductErrors.NotFound);

            return product;
        }
    }
}

using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Types.Dtos;
using ECommerce.UseCases.Types.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types.Queries.Handlers
{
    public sealed class GetAllTypesQueryHandler(IReadRepository<ProductTypeEntity> repository)
    : IRequestHandler<GetAllTypesQuery, Result<IReadOnlyList<GetAllTypesResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllTypesResponse>>> Handle(
            GetAllTypesQuery request,
            CancellationToken cancellationToken)
        {
            var types = await repository.ListAsync(new ProductTypeListSpecification(), cancellationToken);
            return Result<IReadOnlyList<GetAllTypesResponse>>.Success(types);
        }
    }
}

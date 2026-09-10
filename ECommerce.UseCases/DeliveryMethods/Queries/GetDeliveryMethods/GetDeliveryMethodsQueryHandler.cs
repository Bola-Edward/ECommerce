using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.DeliveryMethods.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethods
{
    public sealed class GetDeliveryMethodsQueryHandler(IReadRepository<DeliveryMethodEntity> repository)
    : IRequestHandler<GetDeliveryMethodsQuery, Result<IReadOnlyList<DeliveryMethodResponse>>>
    {
        public async Task<Result<IReadOnlyList<DeliveryMethodResponse>>> Handle(
            GetDeliveryMethodsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await repository.ListAsync(
                new DeliveryMethodsListSpecification(request.AvailableOnly),
                cancellationToken);

            return Result<IReadOnlyList<DeliveryMethodResponse>>.Success(items);
        }
    }
}

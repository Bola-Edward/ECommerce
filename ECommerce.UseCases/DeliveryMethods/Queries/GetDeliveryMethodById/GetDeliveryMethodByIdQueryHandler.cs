using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.DeliveryMethods.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethodById
{
    public sealed class GetDeliveryMethodByIdQueryHandler(IReadRepository<DeliveryMethodEntity> repository)
    : IRequestHandler<GetDeliveryMethodByIdQuery, Result<DeliveryMethodResponse>>
    {
        public async Task<Result<DeliveryMethodResponse>> Handle(
            GetDeliveryMethodByIdQuery request,
            CancellationToken cancellationToken)
        {
            var item = await repository.FirstOrDefaultAsync(
                new DeliveryMethodByIdWithProjectionSpecification(request.Id),
                cancellationToken);

            if (item is null)
                return Result<DeliveryMethodResponse>.Failure(DeliveryMethodErrors.NotFound);

            return Result<DeliveryMethodResponse>.Success(item);
        }
    }
}

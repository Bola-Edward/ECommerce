using ECommerce.Domain.Common;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethodById
{
    public sealed record GetDeliveryMethodByIdQuery(Guid Id)
    : IQuery<Result<DeliveryMethodResponse>>;
}

using ECommerce.Domain.Common;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethods
{
    public sealed record GetDeliveryMethodsQuery(
    bool AvailableOnly = true) : IQuery<Result<IReadOnlyList<DeliveryMethodResponse>>>;
}

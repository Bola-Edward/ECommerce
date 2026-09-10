using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Commands.CreateOrder
{

    public sealed record CreateOrderCommand(
        Guid ShippingAddressId,
        Guid DeliveryMethodId) : ICommand<Result<OrderResponse>>;
}

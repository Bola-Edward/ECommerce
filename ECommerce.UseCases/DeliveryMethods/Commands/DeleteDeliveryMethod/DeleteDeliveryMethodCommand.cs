using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Commands.DeleteDeliveryMethod
{
    public sealed record DeleteDeliveryMethodCommand(Guid Id) : ICommand<Result>;
}

using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Commands.HandleStripeWebhook
{
    public sealed record HandleStripeWebhookCommand(
    string JsonBody,
    string StripeSignatureHeader) : ICommand<Result>;
}

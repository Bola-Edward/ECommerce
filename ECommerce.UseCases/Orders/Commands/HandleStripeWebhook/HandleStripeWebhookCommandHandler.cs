using ECommerce.Domain;
using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Commands.HandleStripeWebhook
{
    public sealed class HandleStripeWebhookCommandHandler : IRequestHandler<HandleStripeWebhookCommand, Result>
    {
        private readonly IPaymentService _paymentService;

        private const string PaymentIntentSucceeded = "payment_intent.succeeded";
        private const string PaymentIntentPaymentFailed = "payment_intent.payment_failed";

        public HandleStripeWebhookCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Result> Handle(HandleStripeWebhookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.StripeSignatureHeader))
                return Result.Failure(OrderErrors.InvalidWebhook);

            var parsed = _paymentService.ParseWebhook(request.JsonBody, request.StripeSignatureHeader);

            if (parsed.IsFailure)
                return Result.Failure(parsed.Error);

            var stripeEvent = parsed.Value;

            switch (stripeEvent.EventType)
            {
                case PaymentIntentSucceeded:
                    if (!string.IsNullOrWhiteSpace(stripeEvent.PaymentIntentId))
                        await _paymentService.PaymentSucceeded(stripeEvent.PaymentIntentId, cancellationToken);
                    break;

                case PaymentIntentPaymentFailed:
                    if (!string.IsNullOrWhiteSpace(stripeEvent.PaymentIntentId))
                        await _paymentService.PaymentFailed(stripeEvent.PaymentIntentId, cancellationToken);
                    break;
            }

            return Result.Success();
        }
    }
}

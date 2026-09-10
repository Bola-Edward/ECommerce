using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Commands.HandleStripeWebhook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [AllowAnonymous]
    public class PaymentsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public PaymentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("webhook")]
        [EndpointSummary("Stripe webhook")]
        [EndpointDescription("Receives Stripe events, verifies the Stripe-Signature, and marks orders as paid on payment_intent.succeeded.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Webhook(CancellationToken ct)
        {
            Request.EnableBuffering();

            using var reader = new StreamReader(Request.Body);
            var json = await reader.ReadToEndAsync(ct);
            var signature = Request.Headers["Stripe-Signature"].ToString();

            var result = await _sender.Send(new HandleStripeWebhookCommand(json, signature), ct);

            return result.IsFailure ? BadRequest() : Ok();
        }
    }
}
using ECommerce.API.Models;
using ECommerce.Domain.Constants;
using ECommerce.UseCases.DeliveryMethods.Commands.CreateDeliveryMethod;
using ECommerce.UseCases.DeliveryMethods.Commands.DeleteDeliveryMethod;
using ECommerce.UseCases.DeliveryMethods.Commands.UpdateDeliveryMethod;
using ECommerce.UseCases.DeliveryMethods.Dtos;
using ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethodById;
using ECommerce.UseCases.DeliveryMethods.Queries.GetDeliveryMethods;
using ECommerce.UseCases.Messaging.Apstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class DeliveryMethodsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public DeliveryMethodsController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        [Authorize(Roles = $"{Roles.SuperAdmin}")]
        [EndpointSummary("List delivery methods")]
        [EndpointDescription("Returns delivery methods. Optionally filter to available ones only.")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<DeliveryMethodResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<DeliveryMethodResponse>>>> GetDeliveryMethods([FromQuery] bool? availableOnly, CancellationToken ct)
        {
            var result = await _sender.Send(new GetDeliveryMethodsQuery(availableOnly ?? true), ct);

            return HandleResult(result, ApiMessages.DeliveryMethodsRetrieved);
        }

        [Authorize(Roles = $"{Roles.SuperAdmin}")]
        [HttpGet("{id:guid}")]
        [EndpointSummary("Get delivery method by id")]
        [ProducesResponseType(typeof(ApiResponse<DeliveryMethodResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<DeliveryMethodResponse>>> GetDeliveryMethodById(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new GetDeliveryMethodByIdQuery(id), ct);

            return HandleResult(result, ApiMessages.DeliveryMethodRetrieved);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        [HttpPost]
        [EndpointSummary("Create delivery method")]
        [ProducesResponseType(typeof(ApiResponse<DeliveryMethodResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<DeliveryMethodResponse>>> CreateDeliveryMethod([FromBody] CreateDeliveryMethodCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);

            return HandleResult(result, ApiMessages.DeliveryMethodCreated);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        [HttpPut("{id:guid}")]
        [EndpointSummary("Update delivery method")]
        [ProducesResponseType(typeof(ApiResponse<DeliveryMethodResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<DeliveryMethodResponse>>> UpdateDeliveryMethod(Guid id, [FromBody] UpdateDeliveryMethodRequest request, CancellationToken ct)
        {
            var command = new UpdateDeliveryMethodCommand(
                id,
                request.Name,
                request.Price,
                request.EstimatedDeliveryTime,
                request.Description,
                request.IsAvailable,
                request.DisplayOrder);

            var result = await _sender.Send(command, ct);

            return HandleResult(result, ApiMessages.DeliveryMethodUpdated);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        [HttpDelete("{id:guid}")]
        [EndpointSummary("Delete delivery method")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteDeliveryMethod(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new DeleteDeliveryMethodCommand(id), ct);

            return HandleResult(result, ApiMessages.DeliveryMethodDeleted);
        }
    }


}
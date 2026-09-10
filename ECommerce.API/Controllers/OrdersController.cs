using ECommerce.API.Models;
using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Commands.CancelOrder;
using ECommerce.UseCases.Orders.Commands.CreateOrder;
using ECommerce.UseCases.Orders.Dtos;
using ECommerce.UseCases.Orders.Queries.GetMyOrders;
using ECommerce.UseCases.Orders.Queries.GetOrderById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
    public class OrdersController : ApiControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [EndpointSummary("Create order (checkout)")]
        [EndpointDescription("Creates an order from the authenticated user's basket, then clears the basket.")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.OrderCreated);
        }

        [HttpGet]
        [EndpointSummary("Get current user orders")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<OrderResponse>>>> GetMyOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var result = await _sender.Send(new GetMyOrdersQuery(pageNumber, pageSize), ct);
            return FromPagedResult(result, pageNumber, pageSize, ApiMessages.OrdersRetrieved);
        }

        [HttpGet("{id:guid}")]
        [EndpointSummary("Get order by id")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> GetOrderById(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new GetOrderByIdQuery(id), ct);
            return HandleResult(result, ApiMessages.OrderRetrieved);
        }

        [HttpPost("{id:guid}/cancel")]
        [EndpointSummary("Cancel pending order")]
        [ProducesResponseType(typeof(ApiResponse<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> CancelOrder(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new CancelOrderCommand(id), ct);
            return HandleResult(result, ApiMessages.OrderCancelled);
        }
    }
}
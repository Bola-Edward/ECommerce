
using ECommerce.API.Models;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Products.Commands.CreateProduct;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace ECommerce.API.Controllers
{

    public class ProductsController : ApiControllerBase
    {
        private readonly ISender _sender;
        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet] // api /products
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllProductsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllProductsResponse>>>> GetAll(
        CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetAllProductsQuery(), ct);

            return HandleResult(result);
        }

        [HttpGet("paged")] // api/products/paged
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllProductsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllProductsResponse>>>> Paged([FromQuery] GetPagedProductsQuery query, CancellationToken ct = default)
        {
            var result = await _sender.Send(query, ct);

            return FromPagedResult(result, query.PageNumber, query.PageSize, "Paged products retrieved successfully");
        }



        [HttpGet("{id:guid}")] // api /products/{id}
        [ProducesResponseType(typeof(ApiResponse<GetProductByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<GetProductByIdResponse>>> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id), ct);
            return HandleResult(result);
        }



        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<Guid>>> Create(
            [FromForm] CreateProductCommand command,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(command, ct);
            return HandleCreatedResult(result, nameof(GetById), new { id = result.Value });
        }

    }
}

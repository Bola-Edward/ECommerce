
using ECommerce.API.Models;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerce.API.Controllers
{

    public class ProductsController : ApiControllerBase
    {
        private readonly GetAllProductsQuery _getAllProductsQuery;
        private readonly GetProductByIdQuery _getProductByIdQuery;
        public ProductsController(GetAllProductsQuery getAllProductsQuery, GetProductByIdQuery getProductByIdQuery)
        {
            _getAllProductsQuery = getAllProductsQuery;
            _getProductByIdQuery = getProductByIdQuery;
        }

        [HttpGet] // api/products
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllProductsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllProductsResponse>>>> GetAll(CancellationToken ct = default)
        {
            var result = await _getAllProductsQuery.ExecuteAsync(ct);
            return HandleResult(result);
        }


        [HttpGet("{id:guid}")] // api /products/{id}
        [ProducesResponseType(typeof(ApiResponse<GetProductByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<GetProductByIdResponse>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<GetProductByIdResponse>>> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await _getProductByIdQuery.ExecuteAsync(id, ct);
            return HandleResult(result);
        }
    }
}

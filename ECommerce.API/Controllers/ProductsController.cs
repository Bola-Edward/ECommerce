
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
        [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>> GetAll(CancellationToken ct = default)
        {
            var result = await _getAllProductsQuery.ExecuteAsync(ct);
            return Ok(result.Value);
        }


        [HttpGet("{id:guid}")] // api /products/{id}
        [ProducesResponseType(typeof(GetProductByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductByIdResponse>> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await _getProductByIdQuery.ExecuteAsync(id, ct);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound(result.Error);
            }
        }
    }
}

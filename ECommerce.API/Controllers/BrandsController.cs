
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Brands.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class BrandsController : ApiControllerBase
    {
        private readonly GetAllBrandsQuery _getAllBrandsQuery;

        public BrandsController(GetAllBrandsQuery getAllBrandsQuery)
        {
            _getAllBrandsQuery = getAllBrandsQuery;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllBrandsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<GetAllBrandsResponse>>> GetAll(CancellationToken ct = default)
        {
            var result = await _getAllBrandsQuery.ExecuteAsync(ct);
            return Ok(result.Value);
        }
    }
}

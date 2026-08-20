
using ECommerce.API.Models;
using ECommerce.UseCases.Types.Dtos;
using ECommerce.UseCases.Types.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class TypesController : ApiControllerBase
    {
        private readonly GetAllTypesQuery _getAllTypesQuery;

        public TypesController(GetAllTypesQuery getAllTypesQuery)
        {
            _getAllTypesQuery = getAllTypesQuery;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllTypesResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllTypesResponse>>>> GetAll(CancellationToken ct = default)
        {
            var result = await _getAllTypesQuery.ExecuteAsync(ct);
            return HandleResult(result);
        }
    }
}

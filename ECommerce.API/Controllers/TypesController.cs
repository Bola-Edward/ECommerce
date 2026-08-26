
using ECommerce.API.Models;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Types.Dtos;
using ECommerce.UseCases.Types.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class TypesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public TypesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllTypesResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllTypesResponse>>>> GetAll(CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetAllTypesQuery(), ct);
            return HandleResult(result);
        }
    }
}

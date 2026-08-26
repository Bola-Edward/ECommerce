
using ECommerce.API.Models;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Brands.Queries;
using ECommerce.UseCases.Messaging.Apstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class BrandsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public BrandsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllBrandsResponse>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<GetAllBrandsResponse>>>> GetAll(CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetAllBrandsQuery(), ct);
            return HandleResult(result);
        }
    }
}

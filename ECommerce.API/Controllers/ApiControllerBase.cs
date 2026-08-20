using ECommerce.API.Models;
using ECommerce.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    // 1- automatic model validation
    // 2- automatic binding of request data to action parameters
    // 3- Problem Details for Error Status Codes Formatting
    // 4- Attribute Routing
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<T>.Success(result.Value));
            }

            if (result.Error.Code.Contains("NotFound", StringComparison.OrdinalIgnoreCase))
            {
                var notFoundResponse = ApiResponse<T>.Failure(
                    statusCode: StatusCodes.Status404NotFound,
                    message: result.Error.Message ?? "Resource not found",
                    errors: [result.Error.Code]
                );

                return NotFound(notFoundResponse);
            }
            else
            {
                var badRequestResponse = ApiResponse<T>.Failure(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: result.Error.Message ?? "Bad request",
                    errors: [result.Error.Code]
                );

                return BadRequest(badRequestResponse);
            }
        }
    }
}

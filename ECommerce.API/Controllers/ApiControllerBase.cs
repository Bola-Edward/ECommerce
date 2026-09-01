using ECommerce.API.Models;
using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
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
        protected ActionResult<ApiResponse<T>> HandleResult<T>(
            Result<T> result,
            string? message = null)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<T>.Success(result.Value, message: message));
            }

            return Problem(result);
        }


        protected ActionResult<ApiResponse<IReadOnlyList<T>>> FromPagedResult<T>(
            Result<PagedResult<T>> result,
            int pageNumber,
            int pageSize,
            string successMessage)
        {
            if (result.IsFailure)
                return Problem(result);

            var pagination = new PaginationMeta(
                pageNumber,
                pageSize,
                result.Value.TotalCount);

            var response = ApiResponse<IReadOnlyList<T>>.Success(
                result.Value.Items,
                message: successMessage,
                pagination: pagination);

            return Ok(response);
        }

        protected ActionResult<ApiResponse<T>> HandleCreatedResult<T>(Result<T> result, string actionName, object routeValues, string? message = null)
        {
            if (result.IsFailure)
                return Problem(result);

            return CreatedAtAction(actionName, routeValues, ApiResponse<T>.Success(result.Value, message: message));
        }


        protected ActionResult Problem(Result result)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation =>
                    StatusCodes.Status400BadRequest,

                ErrorType.NotFound =>
                    StatusCodes.Status404NotFound,

                ErrorType.UnAuthorized =>
                    StatusCodes.Status401Unauthorized,

                ErrorType.Forbidden =>
                    StatusCodes.Status403Forbidden,

                ErrorType.Conflict =>
                    StatusCodes.Status409Conflict,

                _ =>
                    StatusCodes.Status500InternalServerError
            };

            var response = ApiResponse<object>.Failure(
                statusCode: statusCode,
                message: result.Error.Message ?? "An error occurred",
                errors: [result.Error.Code]
            );

            return StatusCode(statusCode, response);
        }
    }
}


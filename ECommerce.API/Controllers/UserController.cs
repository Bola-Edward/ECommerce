using ECommerce.API.Models;
using ECommerce.UseCases.Identity.Commands.AddUserAddress;
using ECommerce.UseCases.Identity.Commands.UpdateUserProfile;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Identity.Queries.GetCurrentUser;
using ECommerce.UseCases.Identity.Queries.GetUserAddress;
using ECommerce.UseCases.Messaging.Apstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        [EndpointSummary("Get current user")]
        [EndpointDescription("Returns the authenticated user's profile.")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> GetCurrentUser(CancellationToken ct)
        {
            var result = await _sender.Send(new GetCurrentUserQuery(), ct);
            return HandleResult(result, ApiMessages.CurrentUserRetrieved);
        }

        [HttpPut("me")]
        [EndpointSummary("Update current user profile")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> UpdateUserProfile([FromBody] UpdateUserProfileCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.UserProfileUpdated);
        }

        [HttpGet("me/addresses")]
        [EndpointSummary("Get current user addresses")]
        [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<UserAddressResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<UserAddressResponse>>>> GetUserAddresses(CancellationToken ct)
        {
            var result = await _sender.Send(new GetUserAddressesQuery(), ct);
            return HandleResult(result, ApiMessages.UserAddressesRetrieved);
        }

        [HttpPost("me/addresses")]
        [EndpointSummary("Add address for current user")]
        [ProducesResponseType(typeof(ApiResponse<UserAddressResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<UserAddressResponse>>> AddUserAddress([FromBody] AddUserAddressCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.UserAddressAdded);
        }



    }
}

using ECommerce.API.Filters;
using ECommerce.API.Models;
using ECommerce.UseCases.Identity.Commands.ConfirmEmail;
using ECommerce.UseCases.Identity.Commands.Login;
using ECommerce.UseCases.Identity.Commands.Logout;
using ECommerce.UseCases.Identity.Commands.RefreshToken;
using ECommerce.UseCases.Identity.Commands.Register;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        [EndpointSummary("Register a new user")]
        [EndpointDescription("Creates an unconfirmed account and emails a verification code. Does not return a JWT.")]
        [ProducesResponseType(typeof(ApiResponse<EmailSentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<EmailSentResponse>>> Register([FromBody] RegisterCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, result.IsSuccess ? result.Value.Message : null);
        }

        [HttpPost("confirm-email")]
        [EndpointSummary("Confirm email with verification code")]
        [EndpointDescription("Validates the code and returns access + refresh tokens.")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> ConfirmEmail([FromBody] ConfirmEmailCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.EmailConfirmed);
        }

        [HttpPost("login")]
        [EndpointSummary("Login")]
        [EndpointDescription("Returns access + refresh tokens for a confirmed account.")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.LoggedIn);
        }

        [HttpPost("refresh")]
        [EndpointSummary("Refresh tokens")]
        [EndpointDescription("Exchanges a valid refresh token for a new access token and rotated refresh token.")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.TokenRefreshed);
        }

        [HttpPost("logout")]
        [EndpointSummary("Logout")]
        [EndpointDescription("Revokes the provided refresh token.")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<object>>> Logout([FromBody] LogoutCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return HandleResult(result, ApiMessages.LoggedOut);
        }
    }
}

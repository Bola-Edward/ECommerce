using ECommerce.UseCases.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestAuthController(
    IJwtTokenGenerator jwtTokenGenerator,
    IWebHostEnvironment environment) : ControllerBase
    {
        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] GenerateTestJwtRequest request)
        {

            if (!environment.IsDevelopment())
                return NotFound();


            var token = jwtTokenGenerator.GenerateToken(
                request.UserId,
                request.Email,
                request.DisplayName,
                request.Roles);

            return Ok(new { Token = token });
        }


        public sealed record GenerateTestJwtRequest(
        Guid UserId,
        string? DisplayName,
        string Email,
        string[] Roles);
    }
}

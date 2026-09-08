using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Common.Models;
using ECommerce.UseCases.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Infrastructure.Identity
{
    public sealed class JwtTokenGenerator(IOptions<JwtSettings> settings) : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings = settings.Value;

        public AccessTokenResult GenerateToken(
            Guid userId,
            string email,
            string? displayName,
            IEnumerable<string>? roles)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>
        {
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, email),
            new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.NameIdentifier, userId.ToString())
        };

            if (!string.IsNullOrWhiteSpace(displayName))
            {
                claims.Add(new Claim("display_name", displayName));
            }

            if (roles is not null)
            {
                claims.AddRange(roles
                    .Where(role => !string.IsNullOrWhiteSpace(role))
                    .Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt.UtcDateTime,
                SigningCredentials = credentials
            };

            var handler = new JsonWebTokenHandler();
            var written = handler.CreateToken(tokenDescriptor);

            return new AccessTokenResult(written, expiresAt);
        }
    }
}

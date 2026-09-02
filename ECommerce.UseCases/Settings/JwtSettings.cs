using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Settings
{
    public sealed class JwtSettings
    {
        public const string SectionName = "Jwt";

        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int AccessTokenExpirationMinutes { get; set; } = 15;
        public int RefreshTokenExpirationDays { get; set; } = 7;
    }
}

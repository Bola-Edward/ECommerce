using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Models
{
    public record AccessTokenResult(string Token, DateTimeOffset ExpiresAtUtc);
}

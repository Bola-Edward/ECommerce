using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Models
{
    public sealed record RefreshTokenIssueResult(
    Guid UserId,
    string Token,
    DateTimeOffset ExpiresAtUtc);
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Interfaces
{
    public record AccessTokenResult(string AccessToken, DateTimeOffset ExpriesAtUtc);
}

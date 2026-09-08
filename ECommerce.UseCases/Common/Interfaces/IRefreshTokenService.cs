using ECommerce.Domain.Common;
using ECommerce.UseCases.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Interfaces
{

    public interface IRefreshTokenService
    {
        Task<RefreshTokenIssueResult> IssueAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<Result<RefreshTokenIssueResult>> RotateAsync(
            string refreshToken,
            CancellationToken cancellationToken = default);

        Task<Result> RevokeAsync(
            string refreshToken,
            CancellationToken cancellationToken = default);
    }
}

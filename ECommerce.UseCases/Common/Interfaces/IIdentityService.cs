using ECommerce.Domain.Common;
using ECommerce.UseCases.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<AuthUserSnapshot>> CreateUserAsync(
            string email,
            string password,
            string? displayName,
            CancellationToken cancellationToken = default);

        Task<Result<AuthUserSnapshot>> ValidateCredentialsAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default);

        Task<Result<AuthUserSnapshot>> GetUserByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<Result<AuthUserSnapshot>> GetUserByIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<Result<AuthUserSnapshot>> UpdateProfileAsync(
            Guid userId,
            string? displayName,
            CancellationToken cancellationToken = default);

        Task<Result> ConfirmEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> IsEmailConfirmedAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<string>> GetRolesAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}

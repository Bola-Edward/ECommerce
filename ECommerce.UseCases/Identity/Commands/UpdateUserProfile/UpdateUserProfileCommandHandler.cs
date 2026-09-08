using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Commands.UpdateUserProfile
{
    public sealed class UpdateUserProfileCommandHandler(
    ICurrentUserService currentUser,
    IIdentityService identityService)
    : IRequestHandler<UpdateUserProfileCommand, Result<UserProfileResponse>>
    {
        public async Task<Result<UserProfileResponse>> Handle(
            UpdateUserProfileCommand request,
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null)
                return Result<UserProfileResponse>.Failure(IdentityErrors.InvalidCredentials);

            var updateResult = await identityService.UpdateProfileAsync(
                currentUser.UserId.Value,
                request.DisplayName,
                cancellationToken);

            if (updateResult.IsFailure)
                return Result<UserProfileResponse>.Failure(updateResult.Error);

            var user = updateResult.Value;
            return Result<UserProfileResponse>.Success(
                new UserProfileResponse(user.UserId, user.Email, user.DisplayName));
        }
    }
}

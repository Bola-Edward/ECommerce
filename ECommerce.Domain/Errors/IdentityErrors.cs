using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Errors
{

    public static class IdentityErrors
    {
        public static readonly Error EmailAlreadyExists =
            Error.Conflict("Identity.EmailAlreadyExists", "An account with this email already exists.");

        public static readonly Error EmailAlreadyConfirmed =
            Error.Conflict("Identity.EmailAlreadyConfirmed", "This email is already confirmed.");

        public static readonly Error EmailNotConfirmed =
            Error.Forbidden("Identity.EmailNotConfirmed", "Confirm your email before signing in.");

        public static readonly Error InvalidCredentials =
            Error.UnAuthorized("Identity.InvalidCredentials", "Invalid email or password.");

        public static readonly Error InvalidVerificationCode =
            Error.Validation("Identity.InvalidVerificationCode", "The verification code is invalid or has expired.");

        public static readonly Error UserNotFound =
            Error.NotFound("Identity.UserNotFound", "User was not found.");

        public static readonly Error EmailSendFailed =
            Error.Failure("Identity.EmailSendFailed", "Failed to send email. Please try again later.");

        public static readonly Error CreateUserFailed =
            Error.Validation("Identity.CreateUserFailed", "Failed to create user account.");

        public static readonly Error UpdateProfileFailed =
            Error.Validation("Identity.UpdateProfileFailed", "Failed to update user profile.");

        public static readonly Error InvalidRefreshToken =
            Error.UnAuthorized("Identity.InvalidRefreshToken", "The refresh token is invalid.");

        public static readonly Error RefreshTokenExpired =
            Error.UnAuthorized("Identity.RefreshTokenExpired", "The refresh token has expired.");


        public static Error CreateFailed(string details) =>
            Error.Validation("Identity.CreateUserFailed", details);

        public static Error UpdateFailed(string details) =>
            Error.Validation("Identity.UpdateProfileFailed", details);
    }
}

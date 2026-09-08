using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Commands.Logout
{
    public sealed record LogoutCommand(
    string RefreshToken) : ICommand<Result>;
}

using ECommerce.Domain.Common;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Commands.Login
{
    public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<Result<AuthResponse>>;
}

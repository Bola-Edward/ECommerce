using ECommerce.Domain.Common;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Commands.AddUserAddress
{
    public sealed record AddUserAddressCommand(
    string Label,
    string RecipientFirstName,
    string RecipientLastName,
    string PhoneNumber,
    string Country,
    string City,
    string Street,
    string PostalCode,
    bool IsDefaultShipping = false,
    bool IsDefaultBilling = false) : ICommand<Result<UserAddressResponse>>;
}

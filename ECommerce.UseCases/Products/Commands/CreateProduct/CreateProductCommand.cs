using ECommerce.Domain.Common;
using ECommerce.UseCases.Messaging.Apstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    IFormFile Image,
    Guid ProductBrandId,
    Guid ProductTypeId) : ICommand<Result<Guid>>;


}

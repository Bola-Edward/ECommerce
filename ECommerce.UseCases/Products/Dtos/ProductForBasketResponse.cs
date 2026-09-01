using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Dtos
{
    public record ProductForBasketResponse(Guid Id, string Name, string PictureUrl, decimal Price);
}

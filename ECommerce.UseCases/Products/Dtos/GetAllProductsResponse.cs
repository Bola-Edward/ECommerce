using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Dtos
{
    public record GetAllProductsResponse(Guid Id, string Name, string Description, decimal Price, string PictureUrl, string ProductType, string ProductBrand);

}

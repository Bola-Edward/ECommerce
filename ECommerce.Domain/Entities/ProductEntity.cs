using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        // todo Store the picture on the cloud => Azure Blob Storage, Cloudinary, Cloudflare R2, etc. and store the URL in the database
        public string PictureUrl { get; private set; } = null!;
        public decimal Price { get; private set; }

        // start writing relationship properties
        public Guid BrandId { get; set; }
        public ProductBrandEntity Brand { get; private set; } = null!;

        public Guid ProductTypeId { get; set; }
        public ProductTypeEntity ProductType { get; private set; } = null!;
    }
}

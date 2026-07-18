using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class ProductBrandEntity : BaseEntity
    {
        public string Name { get; private set; } = null!;

        // start writing relationship properties
        public ICollection<ProductEntity> Products { get; private set; } = new List<ProductEntity>();
    }
}

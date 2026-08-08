using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class ProductTypeEntity : BaseEntity
    {
        public string Name { get; private set; } = null!;
        // start writing relationship properties
        public ICollection<ProductEntity> Products { get; private set; } = new List<ProductEntity>();


        private ProductTypeEntity() { }

        public static ProductTypeEntity Create(Guid id, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Brand id is required", nameof(id));
            }

            return new ProductTypeEntity
            {
                Id = id,
                Name = name.Trim()
            };
        }
    }
}

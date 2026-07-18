using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrandEntity>
    {
        public void Configure(EntityTypeBuilder<ProductBrandEntity> builder)
        {

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(b => b.Name)
                .IsUnique();
        }
    }
}

using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public sealed class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethodEntity>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethodEntity> builder)
        {


            builder.ToTable("DeliveryMethods");


            builder.Property(entity => entity.IsDeleted)
           .HasDefaultValue(false);

            builder.HasQueryFilter(entity => !entity.IsDeleted);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(DeliveryMethodEntity.MaxNameLength);

            builder.Property(x => x.Description)
                .HasMaxLength(DeliveryMethodEntity.MaxDescriptionLength);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.EstimatedDeliveryTime)
                .IsRequired()
                .HasMaxLength(DeliveryMethodEntity.MaxDeliveryTimeLength);

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.DisplayOrder);
        }
    }
}

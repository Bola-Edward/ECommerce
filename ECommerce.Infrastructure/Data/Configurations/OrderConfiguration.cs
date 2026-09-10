using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Orders");

            builder.Property(entity => entity.IsDeleted)
           .HasDefaultValue(false);

            builder.HasQueryFilter(entity => !entity.IsDeleted);


            builder.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.DeliveryMethodName)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxDeliveryMethodNameLength);

            builder.Property(x => x.DeliveryMethodPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.DeliveryMethodEstimatedTime)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxDeliveryTimeLength);

            builder.Property(x => x.ShippingRecipientFirstName)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxNameLength);

            builder.Property(x => x.ShippingRecipientLastName)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxNameLength);

            builder.Property(x => x.ShippingPhoneNumber)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxPhoneLength);

            builder.Property(x => x.ShippingCountry)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxCountryLength);

            builder.Property(x => x.ShippingCity)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxCityLength);

            builder.Property(x => x.ShippingStreet)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxStreetLength);

            builder.Property(x => x.ShippingPostalCode)
                .IsRequired()
                .HasMaxLength(OrderEntity.MaxPostalCodeLength);

            builder.Property(x => x.SubTotal).HasPrecision(18, 2);
            builder.Property(x => x.ShippingCost).HasPrecision(18, 2);
            builder.Property(x => x.Total).HasPrecision(18, 2);

            builder.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Items)
                .HasField("_items")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedAt);
        }
    }

}

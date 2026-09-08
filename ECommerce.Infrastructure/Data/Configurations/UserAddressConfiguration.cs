using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public sealed class UserAddressConfiguration : IEntityTypeConfiguration<UserAddressEntity>
    {
        public void Configure(EntityTypeBuilder<UserAddressEntity> builder)
        {
            builder.ToTable("UserAddresses");

            builder.Property(entity => entity.IsDeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(entity => !entity.IsDeleted);

            builder.Property(x => x.Label)
                .HasMaxLength(UserAddressEntity.MaxNameLength)
                .IsRequired();

            builder.Property(x => x.RecipientFirstName)
                .HasMaxLength(UserAddressEntity.MaxNameLength)
                .IsRequired();

            builder.Property(x => x.RecipientLastName)
                .HasMaxLength(UserAddressEntity.MaxNameLength)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(UserAddressEntity.MaxPhoneLength)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(UserAddressEntity.MaxCountryLength)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(UserAddressEntity.MaxCityLength)
                .IsRequired();

            builder.Property(x => x.Street)
                .HasMaxLength(UserAddressEntity.MaxStreetLength)
                .IsRequired();

            builder.Property(x => x.PostalCode)
                .HasMaxLength(UserAddressEntity.MaxPostalCodeLength)
                .IsRequired();

            builder.HasIndex(x => x.UserId);
        }
    }
}

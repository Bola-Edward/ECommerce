using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {

            builder.HasKey(p => p.Id);


            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.PictureUrl)
                .IsRequired()
                .HasMaxLength(500);


            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Price);


            //Relationships


            builder.HasOne(p => p.ProductBrand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductBrandId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(p => p.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

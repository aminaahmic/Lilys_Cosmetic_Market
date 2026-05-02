using Lilys_CM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lilys_CM.Infrastructure.Database.Configurations.Catalog;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImageEntity>
{
    public void Configure(EntityTypeBuilder<ProductImageEntity> entity)
    {
        entity.ToTable("ProductImages");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(x => x.IsMain)
            .IsRequired();

        entity.Property(x => x.SortOrder)
            .IsRequired();

        entity.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
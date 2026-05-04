using Lilys_CM.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lilys_CM.Infrastructure.Database.Configurations.Catalog;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> b)
    {
        b.ToTable("Products");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Sku)
            .IsRequired()
            .HasMaxLength(64);

        b.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(160);

        b.Property(x => x.ImageUrl)
            .HasMaxLength(500);

        b.Property(x => x.ShortDescription)
            .HasMaxLength(500);

        b.Property(x => x.Description)
            .HasMaxLength(4000);

        b.Property(x => x.Ingredients)
            .HasMaxLength(4000);

        b.Property(x => x.HowToUse)
            .HasMaxLength(2000);

        b.Property(x => x.Benefits)
            .HasMaxLength(2000);

        b.Property(x => x.Brand)
            .HasMaxLength(120);


        b.Property(x => x.Size)
            .HasMaxLength(50);

        b.Property(x => x.CountryOfOrigin)
            .HasMaxLength(120);

        b.Property(x => x.Barcode)
            .HasMaxLength(100);

        b.Property(x => x.SeoTitle)
            .HasMaxLength(200);

        b.Property(x => x.SeoDescription)
            .HasMaxLength(500);

        b.Property(x => x.Price)
            .HasPrecision(18, 2);

        b.Property(x => x.CompareAtPrice)
            .HasPrecision(18, 2);

        b.Property(x => x.StockQuantity)
            .HasDefaultValue(0);

        b.Property(x => x.IsEnabled)
            .HasDefaultValue(true);

        b.Property(x => x.IsFeatured)
            .HasDefaultValue(false);

        b.HasIndex(x => x.Sku)
            .IsUnique();

        b.HasIndex(x => x.Slug)
            .IsUnique();

        b.HasIndex(x => x.Name);

        b.HasIndex(x => x.Brand);

        b.HasOne(x => x.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Subcategory)
            .WithMany(s => s.Products)
            .HasForeignKey(x => x.SubcategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        b.HasOne(x => x.BrandEntity)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
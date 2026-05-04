using Lilys_CM.Domain.Entities.Tokens;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Payments;
using Lilys_CM.Domain.Entities.Reviews;
using Lilys_CM.Domain.Entities.Wishlist;
using Lilys_CM.Domain.Entities.Notifications;
using Lilys_CM.Domain.Entities.Localization;
using Lilys_CM.Domain.Localization;
using Lilys_CM.Domain.Entities;
using Lilys_CM.Domain.Catalog;
using Lilys_CM.Domain.Entities.Catalog;

namespace Lilys_CM.Application.Abstractions
{
    public interface IAppDbContext
    {
        // 🔹 Catalog
        DbSet<ProductEntity> Products { get; }
        DbSet<BrandEntity> Brands { get; }
        DbSet<CategoryEntity> Categories { get; }
        DbSet<SubcategoryEntity> Subcategories { get; }
        DbSet<ProductVariantEntity> ProductVariants { get; }
        DbSet<VariantOptionEntity> VariantOptionEntities { get; }
        DbSet<OptionEntity> Options { get; }
        DbSet<OptionValueEntity> OptionValueEntities { get; }
        DbSet<ProductStockMovementEntity> ProductStockMovements { get; }
        DbSet<ProductImageEntity> ProductImages { get; }

        DbSet<UserEntity> Users { get; }
        DbSet<RoleEntity> Roles { get; }


        DbSet<AddressEntity> Addresses { get; }
        DbSet<CountryEntity> Countries { get; }
        DbSet<CurrencyEntity> Currencies { get; }


        DbSet<NotificationEntity> Notifications { get; }
        DbSet<NotificationTypeEntity> NotificationTypes { get; }


        DbSet<OrderEntity> Orders { get; }
        DbSet<OrderItemEntity> OrderItems { get; }
        DbSet<OrderStatusEntity> OrderStatuses { get; }
        DbSet<PaymentMethodEntity> PaymentMethods { get; }
        DbSet<PaymentTransactionEntity> PaymentTransactions { get; }
        DbSet<PaymentStatusEntity> PaymentStatuses { get; }


        DbSet<ReviewEntity> Reviews { get; }
        DbSet<WishlistEntity> Wishlists { get; }
        DbSet<WishlistProductEntity> WishlistItems { get; }


        DbSet<RefreshTokenEntity> RefreshTokens { get; }
        DbSet<PasswordResetTokenEntity> PasswordResetTokens { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

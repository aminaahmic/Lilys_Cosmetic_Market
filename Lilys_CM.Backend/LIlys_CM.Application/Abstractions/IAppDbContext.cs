using Lilys_CM.Domain.Entities.Tokens;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Payments;
using Lilys_CM.Domain.Entities.Reviews;
using Lilys_CM.Domain.Entities.Wishlist;
using Lilys_CM.Domain.Entities.Notifications;
using Lilys_CM.Domain.Entities.Localization;
using Lilys_CM.Domain.Localization;

namespace Lilys_CM.Application.Abstractions
{
    public interface IAppDbContext
    {
        // 🔹 Catalog
        DbSet<ProductEntity> Products { get; }
        DbSet<CategoryEntity> Categories { get; }
        DbSet<SubcategoryEntity> Subcategories { get; }
        DbSet<ProductVariantEntity> ProductVariants { get; }
        DbSet<VariantOptionEntity> VariantOptionEntities { get; }
        DbSet<OptionValueEntity> OptionValueEntities { get; }

        // 🔹 Identity
        DbSet<UserEntity> Users { get; }
        DbSet<RoleEntity> Roles { get; }

        // 🔹 Localization
        DbSet<AddressEntity> Addresses { get; }
        DbSet<CountryEntity> Countries { get; }
        DbSet<CurrencyEntity> Currencies { get; }

        // 🔹 Notifications
        DbSet<NotificationEntity> Notifications { get; }
        DbSet<NotificationTypeEntity> NotificationTypes { get; }

        // 🔹 Orders & Payments
        DbSet<OrderEntity> Orders { get; }
        DbSet<OrderItemEntity> OrderItems { get; }
        DbSet<OrderStatusEntity> OrderStatuses { get; }
        DbSet<PaymentMethodEntity> PaymentMethods { get; }
        DbSet<PaymentTransactionEntity> PaymentTransactions { get; }
        DbSet<PaymentStatusEntity> PaymentStatuses { get; }

        // 🔹 Reviews & Wishlist
        DbSet<ReviewEntity> Reviews { get; }
        DbSet<WishlistEntity> Wishlists { get; }
        DbSet<WishlistProductEntity> WishlistItems { get; }

        // 🔹 Tokens
        DbSet<RefreshTokenEntity> RefreshTokens { get; }
        DbSet<PasswordResetTokenEntity> PasswordResetTokens { get; }

        // 🔹 EF Core SaveChanges
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

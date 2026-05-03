using Lilys_CM.Application.Abstractions;
using Lilys_CM.Domain.Catalog;
using Lilys_CM.Domain.Entities;
using Lilys_CM.Domain.Entities.Catalog;
using Lilys_CM.Domain.Entities.Identity;
using Lilys_CM.Domain.Entities.Localization;
using Lilys_CM.Domain.Entities.Notifications;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Payments;
using Lilys_CM.Domain.Entities.Reviews;
using Lilys_CM.Domain.Entities.Tokens;
using Lilys_CM.Domain.Entities.Wishlist;
using Lilys_CM.Domain.Localization;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Lilys_CM.Infrastructure.Database
{
    public partial class DatabaseContext : DbContext, IAppDbContext
    {
        private readonly TimeProvider _timeProvider;


        public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider timeProvider)
            : base(options)
        {
            _timeProvider = timeProvider;
        }

        // 🔹 DbSet-ovi (tvoje entitete)
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<SubcategoryEntity> Subcategories => Set<SubcategoryEntity>();
        public DbSet<ProductVariantEntity> ProductVariants => Set<ProductVariantEntity>();
        public DbSet<VariantOptionEntity> VariantOptionEntities => Set<VariantOptionEntity>();
        public DbSet<OptionEntity> Options => Set<OptionEntity>();
        public DbSet<ProductImageEntity> ProductImages => Set<ProductImageEntity>();
        public DbSet<OptionValueEntity> OptionValueEntities => Set<OptionValueEntity>(); public DbSet<ProductStockMovementEntity> ProductStockMovements => Set<ProductStockMovementEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<AddressEntity> Addresses => Set<AddressEntity>();
        public DbSet<CountryEntity> Countries => Set<CountryEntity>();
        public DbSet<CurrencyEntity> Currencies => Set<CurrencyEntity>();
        public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();
        public DbSet<NotificationTypeEntity> NotificationTypes => Set<NotificationTypeEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();
        public DbSet<OrderStatusEntity> OrderStatuses => Set<OrderStatusEntity>();
        public DbSet<PaymentMethodEntity> PaymentMethods => Set<PaymentMethodEntity>();
        public DbSet<PaymentTransactionEntity> PaymentTransactions => Set<PaymentTransactionEntity>();
        public DbSet<PaymentStatusEntity> PaymentStatuses => Set<PaymentStatusEntity>();
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<WishlistEntity> Wishlists => Set<WishlistEntity>();
        public DbSet<WishlistProductEntity> WishlistItems => Set<WishlistProductEntity>();
        public DbSet<PasswordResetTokenEntity> PasswordResetTokens => Set<PasswordResetTokenEntity>();
        public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

        // 🔹 SaveChanges s audit + TimeProvider umjesto DateTime.UtcNow
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = _timeProvider.GetUtcNow().UtcDateTime;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = now;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedAtUtc = now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModifiedAtUtc = now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}

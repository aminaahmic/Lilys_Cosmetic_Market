using Lilys_CM.Domain.Entities.Localization;
using Lilys_CM.Domain.Entities.Notifications;
using Lilys_CM.Domain.Entities.Orders;
using Lilys_CM.Domain.Entities.Payments;
using Lilys_CM.Domain.Localization;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lilys_CM.Infrastructure.Database.Seeders
{
    public static partial class StaticDataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // 🔹 Lookup tabele – statički podaci
            SeedCurrencies(modelBuilder);
            SeedPaymentMethods(modelBuilder);
            SeedOrderStatuses(modelBuilder);
            SeedPaymentStatuses(modelBuilder);
            SeedNotificationTypes(modelBuilder);
            SeedCountries(modelBuilder);
        }

        private static readonly DateTime CreatedAt = DateTime.UtcNow;

        private static void SeedCurrencies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyEntity>().HasData(
                new { Id = 1, Name = "US Dollar", Code = "USD", Symbol = "$", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Euro", Code = "EUR", Symbol = "€", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "Bosnia and Herzegovina Convertible Mark", Code = "BAM", Symbol = "KM", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Croatian Kuna", Code = "HRK", Symbol = "kn", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 5, Name = "Serbian Dinar", Code = "RSD", Symbol = "RSD", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 6, Name = "British Pound", Code = "GBP", Symbol = "£", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 7, Name = "Japanese Yen", Code = "JPY", Symbol = "¥", Decimals = 0, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 8, Name = "Swiss Franc", Code = "CHF", Symbol = "CHF", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 9, Name = "Canadian Dollar", Code = "CAD", Symbol = "$", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 10, Name = "Australian Dollar", Code = "AUD", Symbol = "$", Decimals = 2, CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }

        private static void SeedPaymentMethods(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethodEntity>().HasData(
                new { Id = 1, Name = "Credit Card", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Debit Card", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "PayPal", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Cash on Delivery", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 5, Name = "Bank Transfer", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 6, Name = "Stripe", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 7, Name = "Apple Pay", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 8, Name = "Google Pay", CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }

        private static void SeedOrderStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatusEntity>().HasData(
                new { Id = 1, Name = "Pending", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Processing", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "Shipped", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Delivered", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 5, Name = "Cancelled", CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }

        private static void SeedPaymentStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentStatusEntity>().HasData(
                new { Id = 1, Name = "Pending", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Paid", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "Failed", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Refunded", CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }

        private static void SeedNotificationTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationTypeEntity>().HasData(
                new { Id = 1, Name = "Info", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Warning", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "System", CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Marketing", CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }

        private static void SeedCountries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryEntity>().HasData(
                new { Id = 1, Name = "Bosnia and Herzegovina", IsoCode2 = "BA", IsoCode3 = "BIH", CurrencyId = 3, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 2, Name = "Croatia", IsoCode2 = "HR", IsoCode3 = "HRV", CurrencyId = 4, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 3, Name = "Serbia", IsoCode2 = "RS", IsoCode3 = "SRB", CurrencyId = 5, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 4, Name = "Germany", IsoCode2 = "DE", IsoCode3 = "DEU", CurrencyId = 2, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 5, Name = "United States", IsoCode2 = "US", IsoCode3 = "USA", CurrencyId = 1, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 6, Name = "United Kingdom", IsoCode2 = "GB", IsoCode3 = "GBR", CurrencyId = 6, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 7, Name = "Japan", IsoCode2 = "JP", IsoCode3 = "JPN", CurrencyId = 7, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 8, Name = "Switzerland", IsoCode2 = "CH", IsoCode3 = "CHE", CurrencyId = 8, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 9, Name = "Canada", IsoCode2 = "CA", IsoCode3 = "CAN", CurrencyId = 9, CreatedAtUtc = CreatedAt, IsDeleted = false },
                new { Id = 10, Name = "Australia", IsoCode2 = "AU", IsoCode3 = "AUS", CurrencyId = 10, CreatedAtUtc = CreatedAt, IsDeleted = false }
            );
        }
    }
}

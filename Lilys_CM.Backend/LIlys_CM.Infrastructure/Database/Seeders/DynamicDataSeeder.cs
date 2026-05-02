using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lilys_CM.Domain.Entities.Catalog;
using Lilys_CM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Infrastructure.Database.Seeders
{
    public static class DynamicDataSeeder
    {
        public static async Task SeedAsync(DatabaseContext context)
        {
            await context.Database.EnsureCreatedAsync();

            await SeedCategoriesAsync(context);
            await SeedSubcategoriesAsync(context);
            await SeedProductsAsync(context);
            await SeedUsersAsync(context);
        }

        private static async Task SeedCategoriesAsync(DatabaseContext context)
        {
            if (await context.Products.AnyAsync())
            {
                return;
            }

            context.Categories.AddRange(
                new CategoryEntity
                {
                    Name = "Skincare",
                    IsEnabled = true,
                    CreatedAtUtc = DateTime.UtcNow
                },
                new CategoryEntity
                {
                    Name = "Makeup",
                    IsEnabled = true,
                    CreatedAtUtc = DateTime.UtcNow
                },
                new CategoryEntity
                {
                    Name = "Haircare",
                    IsEnabled = true,
                    CreatedAtUtc = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();
            Console.WriteLine("Dynamic seed: demo categories added.");
        }

        private static async Task SeedSubcategoriesAsync(DatabaseContext context)
        {
            if (await context.Subcategories.AnyAsync())
            {
                return;
            }

            var categories = await context.Categories
                .AsNoTracking()
                .ToDictionaryAsync(c => c.Name, c => c.Id);

            if (categories.Count == 0)
            {
                return;
            }

            var now = DateTime.UtcNow;

            int GetCategoryOrFallback(string name)
            {
                if (categories.TryGetValue(name, out var id))
                {
                    return id;
                }

                return categories.Values.First();
            }

            var subcategories = new List<SubcategoryEntity>
            {
                new() { Name = "Cleansers", CategoryId = GetCategoryOrFallback("Skincare"), CreatedAtUtc = now },
                new() { Name = "Serums", CategoryId = GetCategoryOrFallback("Skincare"), CreatedAtUtc = now },
                new() { Name = "Moisturizers", CategoryId = GetCategoryOrFallback("Skincare"), CreatedAtUtc = now },
                new() { Name = "Foundations", CategoryId = GetCategoryOrFallback("Makeup"), CreatedAtUtc = now },
                new() { Name = "Lipsticks", CategoryId = GetCategoryOrFallback("Makeup"), CreatedAtUtc = now },
                new() { Name = "Mascaras", CategoryId = GetCategoryOrFallback("Makeup"), CreatedAtUtc = now },
                new() { Name = "Shampoos", CategoryId = GetCategoryOrFallback("Haircare"), CreatedAtUtc = now },
                new() { Name = "Conditioners", CategoryId = GetCategoryOrFallback("Haircare"), CreatedAtUtc = now },
                new() { Name = "Hair Masks", CategoryId = GetCategoryOrFallback("Haircare"), CreatedAtUtc = now }
            };

            context.Subcategories.AddRange(subcategories);
            await context.SaveChangesAsync();

            Console.WriteLine("Dynamic seed: demo subcategories added.");
        }

        private static async Task SeedProductsAsync(DatabaseContext context)
        {

            if (await context.Products.AnyAsync())
            {
                return;
            }
            var categories = await context.Categories
                .AsNoTracking()
                .ToDictionaryAsync(c => c.Name, c => c.Id);

            var subcategories = await context.Subcategories
                .AsNoTracking()
                .ToDictionaryAsync(s => s.Name, s => s.Id);

            if (categories.Count == 0)
            {
                return;
            }

            var now = DateTime.UtcNow;

            int GetCategoryOrFallback(string name)
            {
                if (categories.TryGetValue(name, out var id))
                {
                    return id;
                }

                return categories.Values.First();
            }

            int? GetSubcategoryOrNull(string name)
            {
                if (subcategories.TryGetValue(name, out var id))
                {
                    return id;
                }

                return null;
            }

            var products = new List<ProductEntity>
    {
        new()
        {
            Name = "Hydra Glow Cleanser",
            Sku = "SKIN-CLN-001",
            Slug = "hydra-glow-cleanser",
            ImageUrl = "https://images.unsplash.com/photo-1556228578-8c89e6adf883",
            ShortDescription = "Gentle gel cleanser for daily cleansing without drying the skin.",
            Description = "A lightweight facial cleanser that removes makeup residue, sunscreen and daily impurities while keeping the skin soft and balanced.",
            Ingredients = "Aqua, Glycerin, Cocamidopropyl Betaine, Panthenol, Allantoin",
            HowToUse = "Apply to damp skin, massage gently for 30 seconds, then rinse with lukewarm water.",
            Benefits = "Cleanses gently, supports skin barrier, suitable for everyday use.",
            Brand = "Lily Glow",
            Size = "150ml",
            CountryOfOrigin = "France",
            Barcode = "100000000001",
            Price = 21.90m,
            CompareAtPrice = 26.90m,
            StockQuantity = 35,
            IsEnabled = true,
            IsFeatured = true,
            SeoTitle = "Hydra Glow Cleanser | Lily Glow",
            SeoDescription = "Gentle daily facial cleanser for fresh, clean and comfortable skin.",
            SubcategoryId = GetSubcategoryOrNull("Cleansers"),
            CategoryId = GetCategoryOrFallback("Skincare"),
            CreatedAtUtc = now
        },
        new()
        {
            Name = "Vitamin C Booster",
            Sku = "SKIN-SER-002",
            Slug = "vitamin-c-booster",
            ImageUrl = "https://images.unsplash.com/photo-1620916566398-39f1143ab7be",
            ShortDescription = "Brightening serum with vitamin C for a fresh and radiant complexion.",
            Description = "A concentrated serum designed to visibly improve dull-looking skin and support a more even, glowing appearance.",
            Ingredients = "Aqua, Ascorbic Acid, Glycerin, Ferulic Acid, Sodium Hyaluronate",
            HowToUse = "Apply 2 to 3 drops to clean dry skin in the morning before moisturizer and SPF.",
            Benefits = "Brightens complexion, improves glow, supports smoother-looking skin.",
            Brand = "Derma Bloom",
            Size = "30ml",
            CountryOfOrigin = "Germany",
            Barcode = "100000000002",
            Price = 37.50m,
            CompareAtPrice = 44.90m,
            StockQuantity = 22,
            IsEnabled = true,
            IsFeatured = true,
            SeoTitle = "Vitamin C Booster Serum | Derma Bloom",
            SeoDescription = "Radiance-boosting vitamin C serum for dull and tired-looking skin.",
            SubcategoryId = GetSubcategoryOrNull("Serums"),
            CategoryId = GetCategoryOrFallback("Skincare"),
            CreatedAtUtc = now
        },
        new()
        {
            Name = "Daily Comfort Moisturizer",
            Sku = "SKIN-MOI-003",
            Slug = "daily-comfort-moisturizer",
            ImageUrl = "https://images.unsplash.com/photo-1617897903246-719242758050",
            ShortDescription = "Soft moisturizing cream for normal to dry skin.",
            Description = "A nourishing face cream that helps reduce dryness and leaves the skin feeling soft, smooth and comfortable throughout the day.",
            Ingredients = "Aqua, Glycerin, Shea Butter, Niacinamide, Panthenol",
            HowToUse = "Apply morning and evening to clean face and neck.",
            Benefits = "Hydrates, softens, improves comfort, ideal for daily use.",
            Brand = "Pure Veil",
            Size = "50ml",
            CountryOfOrigin = "Italy",
            Barcode = "100000000003",
            Price = 29.90m,
            CompareAtPrice = 34.90m,
            StockQuantity = 18,
            IsEnabled = true,
            IsFeatured = false,
            SeoTitle = "Daily Comfort Moisturizer | Pure Veil",
            SeoDescription = "Everyday face cream that hydrates and comforts dry skin.",
            SubcategoryId = GetSubcategoryOrNull("Moisturizers"),
            CategoryId = GetCategoryOrFallback("Skincare"),
            CreatedAtUtc = now
        },
        new()
        {
            Name = "Velvet Matte Lipstick",
            Sku = "MAKE-LIP-004",
            Slug = "velvet-matte-lipstick",
            ImageUrl = "https://images.unsplash.com/photo-1586495777744-4413f21062fa",
            ShortDescription = "Rich matte lipstick with smooth, comfortable wear.",
            Description = "A highly pigmented lipstick that gives a velvet-matte finish without making lips feel dry.",
            Ingredients = "Dimethicone, Isododecane, Silica, Synthetic Wax, Tocopherol",
            HowToUse = "Apply directly to lips or use a lip brush for more precise definition.",
            Benefits = "Strong color payoff, smooth finish, comfortable matte look.",
            Brand = "Rose Atelier",
            Size = "4g",
            CountryOfOrigin = "Italy",
            Barcode = "100000000004",
            Price = 18.00m,
            CompareAtPrice = 22.00m,
            StockQuantity = 41,
            IsEnabled = true,
            IsFeatured = true,
            SeoTitle = "Velvet Matte Lipstick | Rose Atelier",
            SeoDescription = "Matte lipstick with rich pigment and elegant finish.",
            SubcategoryId = GetSubcategoryOrNull("Lipsticks"),
            CategoryId = GetCategoryOrFallback("Makeup"),
            CreatedAtUtc = now
        },
        new()
        {
            Name = "Volume Lift Mascara",
            Sku = "MAKE-MAS-005",
            Slug = "volume-lift-mascara",
            ImageUrl = "https://images.unsplash.com/photo-1596462502278-27bfdc403348",
            ShortDescription = "Buildable volume mascara with defined lash separation.",
            Description = "A mascara formula that helps lift lashes, build volume gradually and keep the look neat without visible clumping.",
            Ingredients = "Aqua, Beeswax, Carnauba Wax, Iron Oxides, Panthenol",
            HowToUse = "Apply from lash root to tip in zig-zag motions. Add a second coat for extra volume.",
            Benefits = "Adds volume, lifts lashes, easy layering.",
            Brand = "Rose Atelier",
            Size = "10ml",
            CountryOfOrigin = "Germany",
            Barcode = "100000000005",
            Price = 19.90m,
            CompareAtPrice = 24.90m,
            StockQuantity = 17,
            IsEnabled = true,
            IsFeatured = false,
            SeoTitle = "Volume Lift Mascara | Rose Atelier",
            SeoDescription = "Buildable volume mascara for fuller-looking lashes.",
            SubcategoryId = GetSubcategoryOrNull("Mascaras"),
            CategoryId = GetCategoryOrFallback("Makeup"),
            CreatedAtUtc = now
        },
        new()
        {
            Name = "Silk Repair Shampoo",
            Sku = "HAIR-SHA-006",
            Slug = "silk-repair-shampoo",
            ImageUrl = "https://images.unsplash.com/photo-1527799820374-dcf8d9d4a388",
            ShortDescription = "Repair shampoo for dry and damaged hair.",
            Description = "A strengthening shampoo that gently cleanses while helping hair feel softer, smoother and easier to manage.",
            Ingredients = "Aqua, Sodium Laureth Sulfate, Keratin, Argan Oil, Panthenol",
            HowToUse = "Massage into wet scalp and hair, lather well, then rinse thoroughly.",
            Benefits = "Cleanses, smooths, supports damaged hair care routine.",
            Brand = "Hair Muse",
            Size = "250ml",
            CountryOfOrigin = "Spain",
            Barcode = "100000000006",
            Price = 24.40m,
            CompareAtPrice = 29.90m,
            StockQuantity = 29,
            IsEnabled = true,
            IsFeatured = true,
            SeoTitle = "Silk Repair Shampoo | Hair Muse",
            SeoDescription = "Repairing shampoo for dry, stressed and damaged hair.",
            SubcategoryId = GetSubcategoryOrNull("Shampoos"),
            CategoryId = GetCategoryOrFallback("Haircare"),
            CreatedAtUtc = now
        }
    };

            var existingSkus = await context.Products
              .Select(p => p.Sku)
              .ToListAsync();

            var productsToAdd = products
                .Where(p => !existingSkus.Contains(p.Sku))
                .GroupBy(p => p.Sku)
                .Select(g => g.First())
                .ToList();

            if (!productsToAdd.Any())
            {
                return;
            }

            context.Products.AddRange(productsToAdd);
            await context.SaveChangesAsync();

            Console.WriteLine("Dynamic seed: demo products added.");
        }
        private static async Task SeedUsersAsync(DatabaseContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var hasher = new PasswordHasher<UserEntity>();

            var admin = new UserEntity
            {
                Name = "Admin",
                Email = "admin@lilys.local",
                PasswordHash = hasher.HashPassword(null!, "Admin123!"),
                IsAdmin = true,
                IsCustomer = false,
                IsEnabled = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            var customer = new UserEntity
            {
                Name = "Demo Customer",
                Email = "customer@lilys.local",
                PasswordHash = hasher.HashPassword(null!, "Customer123!"),
                IsAdmin = false,
                IsCustomer = true,
                IsEnabled = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            context.Users.AddRange(admin, customer);
            await context.SaveChangesAsync();

            Console.WriteLine("Dynamic seed: demo users (admin and customer) added.");
        }
    }
}

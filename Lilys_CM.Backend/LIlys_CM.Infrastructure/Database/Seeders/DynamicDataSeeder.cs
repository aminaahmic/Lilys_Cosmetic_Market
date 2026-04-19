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
            if (await context.Categories.AnyAsync())
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

            var products = new List<ProductEntity>
            {
                new()
                {
                    Name = "Hydra Glow Cleanser",
                    Description = "Gentle gel cleanser for daily use.",
                    Brand = "Lily Glow",
                    Subcategory = "Cleansers",
                    Price = 21.90m,
                    StockQuantity = 35,
                    IsEnabled = true,
                    CategoryId = GetCategoryOrFallback("Skincare"),
                    CreatedAtUtc = now
                },
                new()
                {
                    Name = "Vitamin C Booster",
                    Description = "Brightening serum with vitamin C.",
                    Brand = "Derma Bloom",
                    Subcategory = "Serums",
                    Price = 37.50m,
                    StockQuantity = 22,
                    IsEnabled = true,
                    CategoryId = GetCategoryOrFallback("Skincare"),
                    CreatedAtUtc = now
                },
                new()
                {
                    Name = "Velvet Matte Lipstick",
                    Description = "Long-wear lipstick with smooth finish.",
                    Brand = "Rose Atelier",
                    Subcategory = "Lipsticks",
                    Price = 18.00m,
                    StockQuantity = 41,
                    IsEnabled = true,
                    CategoryId = GetCategoryOrFallback("Makeup"),
                    CreatedAtUtc = now
                },
                new()
                {
                    Name = "Silk Repair Shampoo",
                    Description = "Repair shampoo for dry and damaged hair.",
                    Brand = "Hair Muse",
                    Subcategory = "Shampoos",
                    Price = 24.40m,
                    StockQuantity = 29,
                    IsEnabled = true,
                    CategoryId = GetCategoryOrFallback("Haircare"),
                    CreatedAtUtc = now
                },
                new()
                {
                    Name = "Volume Lift Mascara",
                    Description = "Buildable volume with no clumping.",
                    Brand = "Rose Atelier",
                    Subcategory = "Mascaras",
                    Price = 19.90m,
                    StockQuantity = 17,
                    IsEnabled = false,
                    CategoryId = GetCategoryOrFallback("Makeup"),
                    CreatedAtUtc = now
                }
            };

            context.Products.AddRange(products);
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

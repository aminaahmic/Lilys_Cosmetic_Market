using System;
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
            await SeedUsersAsync(context);
        }

        private static async Task SeedCategoriesAsync(DatabaseContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                context.Categories.AddRange(
                    new CategoryEntity
                    {
                        Name = "Računari (demo)",
                        IsEnabled = true,
                        CreatedAtUtc = DateTime.UtcNow
                    },
                    new CategoryEntity
                    {
                        Name = "Mobilni uređaji (demo)",
                        IsEnabled = true,
                        CreatedAtUtc = DateTime.UtcNow
                    }
                );

                await context.SaveChangesAsync();
                Console.WriteLine("✅ Dynamic seed: demo categories added.");
            }
        }

        private static async Task SeedUsersAsync(DatabaseContext context)
        {
            if (await context.Users.AnyAsync())
                return;

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

            Console.WriteLine("✅ Dynamic seed: demo users (admin & customer) added.");
        }
    }
}

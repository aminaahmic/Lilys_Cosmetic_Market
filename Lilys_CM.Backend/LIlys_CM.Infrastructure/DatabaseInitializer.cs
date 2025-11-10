using Lilys_CM.Infrastructure.Database;
using Lilys_CM.Infrastructure.Database.Seeders;
using Lilys_CM.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lilys_CM.Infrastructure;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider services, IHostEnvironment env)
    {
        await using var scope = services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (env.IsTest())
        {
            await ctx.Database.EnsureCreatedAsync();
            await DynamicDataSeeder.SeedAsync(ctx);
            return;
        }

        // SQL Server or similar
        await ctx.Database.MigrateAsync();

        if (env.IsDevelopment())
        {
            await DynamicDataSeeder.SeedAsync(ctx);
        }
    }
}

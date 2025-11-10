using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Lilys_CM.Infrastructure.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // 🔹 1) Nađi glavni API projekt (gdje je appsettings.json)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Lilys_CM.API");

            if (!Directory.Exists(basePath))
                throw new DirectoryNotFoundException($"❌ API folder nije pronađen na lokaciji: {basePath}");

            // 🔹 2) Učitaj konfiguraciju iz appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // 🔹 3) Učitaj connection string
            var connectionString = configuration.GetConnectionString("Main");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("❌ Connection string 'Main' nije pronađen u appsettings.json.");

            // 🔹 4) Konfiguriši EF Core
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // 🔹 5) Vrati instancu konteksta (za migracije)
            return new DatabaseContext(optionsBuilder.Options, TimeProvider.System);
        }
    }
}

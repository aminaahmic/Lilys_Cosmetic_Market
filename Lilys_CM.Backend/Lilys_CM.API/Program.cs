using Lilys_CM.API;
using Lilys_CM.API.Middleware;
using Lilys_CM.Application;
using Lilys_CM.Application.Abstractions.Email;
using Lilys_CM.Infrastructure;
using Lilys_CM.Infrastructure.Email;
using Serilog;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting Lilys_CM API...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton(TimeProvider.System);

            builder.Host.UseSerilog((ctx, services, cfg) =>
            {
                cfg.ReadFrom.Configuration(ctx.Configuration)
                   .ReadFrom.Services(services)
                   .Enrich.FromLogContext()
                   .Enrich.WithThreadId()
                   .Enrich.WithProcessId()
                   .Enrich.WithMachineName();
            });

            builder.Logging.ClearProviders();

            builder.Services
                .AddAPI(builder.Configuration, builder.Environment)
                .AddInfrastructure(builder.Configuration, builder.Environment)
                .AddApplication();

            builder.Services.Configure<PostmarkOptions>(
                builder.Configuration.GetSection("Postmark")
            );

            builder.Services.AddScoped<IEmailSender, PostmarkEmailSender>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowFrontend");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.Services.InitializeDatabaseAsync(app.Environment);

            Log.Information("Lilys_CM API started successfully.");

            await app.RunAsync();
        }
        catch (HostAbortedException)
        {
            Log.Information("Host aborted by EF Core tooling (design-time) - it's ok.");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Lilys_CM API terminated unexpectedly.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
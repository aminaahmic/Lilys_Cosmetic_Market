using Lilys_CM.Infrastructure.Common;
using Lilys_CM.Shared.Dtos;
using Lilys_CM.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Lilys_CM.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(opts =>
            {
                opts.InvalidModelStateResponseFactory = ctx =>
                {
                    var msg = string.Join("; ",
                        ctx.ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                ? "Validation error"
                                : e.ErrorMessage));

                    return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new ErrorDto
                    {
                        Code = "validation.failed",
                        Message = msg
                    });
                };
            });

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwt = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
            ?? throw new InvalidOperationException("Jwt options are not configured.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.Key)
                    ),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    NameClaimType = "email",
                    RoleClaimType = "role"
                };
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lilys_CM API",
                Version = "v1"
            });

            var xml = Path.Combine(AppContext.BaseDirectory, "Lilys_CM.API.xml");
            if (File.Exists(xml))
            {
                c.IncludeXmlComments(xml, includeControllerXmlComments: true);
            }

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Unesi JWT token. Format: Bearer {token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddExceptionHandler<Lilys_CMExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
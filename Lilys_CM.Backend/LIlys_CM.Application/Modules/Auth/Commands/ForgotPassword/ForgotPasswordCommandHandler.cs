using System.Security.Cryptography;
using System.Text;
using Lilys_CM.Application.Abstractions.Email;
using Lilys_CM.Domain.Entities.Tokens;
using Lilys_CM.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Lilys_CM.Application.Common.EmailTemplates;

namespace Lilys_CM.Application.Modules.Auth.Commands.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(
    IAppDbContext ctx,
    IEmailSender emailSender,
    IOptions<FrontendOptions> frontendOptions
)
    : IRequestHandler<ForgotPasswordCommand, ForgotPasswordCommandDto>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly FrontendOptions _frontendOptions = frontendOptions.Value;

    public async Task<ForgotPasswordCommandDto> Handle(ForgotPasswordCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await ctx.Users
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.IsEnabled && !x.IsDeleted, ct);

        
        if (user is null)
        {
            return new ForgotPasswordCommandDto
            {
                Message = "Ako korisnik sa ovim emailom postoji, poslali smo link za reset lozinke."
            };
        }

        
        var activeTokens = await ctx.PasswordResetTokens
            .Where(x => x.UserId == user.Id && !x.Used && !x.IsDeleted)
            .ToListAsync(ct);

        foreach (var item in activeTokens)
        {
            item.Used = true;
        }

        
        var rawToken = GenerateSecureToken();
        var hashedToken = HashToken(rawToken);

        var expires = DateTime.UtcNow.AddMinutes(30);

        ctx.PasswordResetTokens.Add(new PasswordResetTokenEntity
        {
            UserId = user.Id,
            Token = hashedToken, 
            ExpiryDate = expires,
            Used = false
        });

        await ctx.SaveChangesAsync(ct);

        
        var resetLink = $"{_frontendOptions.BaseUrl}/auth/reset-password?token={Uri.EscapeDataString(rawToken)}";

        // EMAIL
        var subject = "Reset your Lily's password";

        var htmlBody = $"""
            <h2>Reset lozinke</h2>
            <p>Primili smo zahtjev za promjenu lozinke za vaš Lily's nalog.</p>
            <p>Kliknite na link ispod:</p>
            <p><a href="{resetLink}">Reset password</a></p>
            <p>Link važi 30 minuta i može se koristiti samo jednom.</p>
            """;
// EMAIL template
//var subject = "Reset your Lily's password";

//var htmlBody = AuthEmailTemplates.BuildResetPasswordEmail(
  //  resetLink,
    //"Lily's Cosmetic Market",
    //30
//);
        await _emailSender.SendAsync(user.Email, subject, htmlBody, ct);

        return new ForgotPasswordCommandDto
        {
            Message = "Ako korisnik postoji, email za reset lozinke je poslan."
        };
    }

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes);
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
}
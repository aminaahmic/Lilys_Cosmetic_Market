using System.Security.Cryptography;
using Lilys_CM.Domain.Entities.Tokens;

namespace Lilys_CM.Application.Modules.Auth.Commands.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(IAppDbContext ctx)
    : IRequestHandler<ForgotPasswordCommand, ForgotPasswordCommandDto>
{
    public async Task<ForgotPasswordCommandDto> Handle(ForgotPasswordCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await ctx.Users
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.IsEnabled && !x.IsDeleted, ct);

        // Ne otkrivamo da li email postoji ili ne
        if (user is null)
        {
            return new ForgotPasswordCommandDto
            {
                Message = "Ako korisnik sa ovim emailom postoji, link za promjenu lozinke je pripremljen."
            };
        }

        // Po želji poništi stare neiskorištene tokene
        var activeTokens = await ctx.PasswordResetTokens
            .Where(x => x.UserId == user.Id && !x.Used && !x.IsDeleted)
            .ToListAsync(ct);

        foreach (var item in activeTokens)
        {
            item.Used = true;
        }

        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        ctx.PasswordResetTokens.Add(new PasswordResetTokenEntity
        {
            UserId = user.Id,
            Token = token,
            ExpiryDate = DateTime.UtcNow.AddMinutes(30),
            Used = false
        });

        await ctx.SaveChangesAsync(ct);

        return new ForgotPasswordCommandDto
        {
            Message = "Reset token je uspješno generisan.",
            ResetToken = token
        };
    }
}
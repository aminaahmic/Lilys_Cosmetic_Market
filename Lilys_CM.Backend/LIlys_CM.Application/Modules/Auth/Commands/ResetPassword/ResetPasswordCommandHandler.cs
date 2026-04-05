using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Lilys_CM.Application.Modules.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandHandler(
    IAppDbContext ctx,
    IPasswordHasher<UserEntity> hasher)
    : IRequestHandler<ResetPasswordCommand, ResetPasswordCommandDto>
{
    public async Task<ResetPasswordCommandDto> Handle(ResetPasswordCommand request, CancellationToken ct)
    {
        var rawToken = request.Token.Trim();
        var hashedToken = HashToken(rawToken);

        var resetToken = await ctx.PasswordResetTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == hashedToken &&   // 🔥 OVDJE JE FIX
                !x.Used &&
                !x.IsDeleted, ct);

        if (resetToken is null)
            throw new Lilys_CMConflictException("Reset token nije validan.");

        if (resetToken.ExpiryDate is null || resetToken.ExpiryDate.Value < DateTime.UtcNow)
            throw new Lilys_CMConflictException("Reset token je istekao.");

        if (resetToken.User is null || !resetToken.User.IsEnabled || resetToken.User.IsDeleted)
            throw new Lilys_CMConflictException("Korisnik nije dostupan za promjenu lozinke.");

        // change password
        resetToken.User.PasswordHash = hasher.HashPassword(resetToken.User, request.NewPassword);

        // invalidate token
        resetToken.Used = true;

        await ctx.SaveChangesAsync(ct);

        return new ResetPasswordCommandDto
        {
            Message = "Lozinka je uspješno promijenjena."
        };
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
}
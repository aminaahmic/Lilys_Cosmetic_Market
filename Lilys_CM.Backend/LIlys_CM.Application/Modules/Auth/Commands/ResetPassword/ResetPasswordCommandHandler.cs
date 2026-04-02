namespace Lilys_CM.Application.Modules.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandHandler(
    IAppDbContext ctx,
    IPasswordHasher<UserEntity> hasher)
    : IRequestHandler<ResetPasswordCommand, ResetPasswordCommandDto>
{
    public async Task<ResetPasswordCommandDto> Handle(ResetPasswordCommand request, CancellationToken ct)
    {
        var tokenValue = request.Token.Trim();

        var resetToken = await ctx.PasswordResetTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == tokenValue &&
                !x.Used &&
                !x.IsDeleted, ct);

        if (resetToken is null)
            throw new Lilys_CMConflictException("Reset token nije validan.");

        if (resetToken.ExpiryDate is null || resetToken.ExpiryDate.Value < DateTime.UtcNow)
            throw new Lilys_CMConflictException("Reset token je istekao.");

        if (resetToken.User is null || !resetToken.User.IsEnabled || resetToken.User.IsDeleted)
            throw new Lilys_CMConflictException("Korisnik nije dostupan za promjenu lozinke.");

        resetToken.User.PasswordHash = hasher.HashPassword(resetToken.User, request.NewPassword);
        resetToken.Used = true;

        await ctx.SaveChangesAsync(ct);

        return new ResetPasswordCommandDto
        {
            Message = "Lozinka je uspješno promijenjena."
        };
    }
}
using Lilys_CM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Lilys_CM.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandHandler(
    IAppDbContext ctx,
    IPasswordHasher<UserEntity> hasher)
    : IRequestHandler<RegisterCommand, RegisterCommandDto>
{
    public async Task<RegisterCommandDto> Handle(RegisterCommand request, CancellationToken ct)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedName = request.Name.Trim();

        var emailExists = await ctx.Users.AnyAsync(
            x => x.Email.ToLower() == normalizedEmail && !x.IsDeleted,
            ct);

        if (emailExists)
            throw new Lilys_CMConflictException("Korisnik sa ovim emailom već postoji.");

        var user = new UserEntity
        {
            Name = normalizedName,
            Email = normalizedEmail,
            PasswordHash = hasher.HashPassword(null!, request.Password),
            IsAdmin = false,
            IsCustomer = true,
            IsEnabled = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        ctx.Users.Add(user);
        await ctx.SaveChangesAsync(ct);

        return new RegisterCommandDto
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Message = "Registracija uspješna. Sada se možete prijaviti."
        };
    }
}
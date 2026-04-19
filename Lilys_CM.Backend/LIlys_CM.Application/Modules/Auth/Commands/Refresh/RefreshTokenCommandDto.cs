namespace Lilys_CM.Application.Modules.Auth.Commands.Refresh;

/// <summary>
/// DTO returned after a successful token refresh (rotation).
/// </summary>
public sealed class RefreshTokenCommandDto
{
    /// <summary>
    /// New access token that the client should use for authentication.
    /// </summary>
    public string AccessToken { get; init; } = default!;

    /// <summary>
    /// New refresh token that replaces the previous one.
    /// </summary>
    public string RefreshToken { get; init; } = default!;

    /// <summary>
    /// Expiration date of the access token in UTC format.
    /// </summary>
    public DateTime AccessTokenExpiresAtUtc { get; init; }

    /// <summary>
    /// Expiration date of the refresh token in UTC format.
    /// </summary>
    public DateTime RefreshTokenExpiresAtUtc { get; init; }
}

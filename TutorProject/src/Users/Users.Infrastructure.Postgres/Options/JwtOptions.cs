namespace Users.Infrastructure.Postgres.Options;

public class JwtOptions()
{
    public const string SECTION_NAME = nameof(JwtOptions);

    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public string SecurityKey { get; init; } = string.Empty;

    public string Expiration { get; init; } = string.Empty;
}
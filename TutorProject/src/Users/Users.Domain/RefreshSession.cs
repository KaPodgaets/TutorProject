using Users.Domain.ValueObjects;

namespace Users.Domain;

public class RefreshSession
{
    public Guid Id { get; init; }

    public required UserId UserId { get; init; }

    public User User { get; init; } = default!;

    public Guid RefreshToken { get; init; }

    public DateTime ExpiresIn { get; init; }

    public DateTime CreatedAt { get; init; }

    public Guid Jti { get; init; }
}
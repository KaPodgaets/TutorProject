using CSharpFunctionalExtensions;
using Users.Domain.ValueObjects;

namespace Users.Domain;

public class Session
{
    public Session(SessionId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }

    public SessionId Id { get; set; }

    public UserId UserId { get; set; }

    public static Session Create(SessionId id, UserId userId)
    {
        return new Session(id, userId);
    }
}

public sealed class SessionId : ComparableValueObject
{
    private SessionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
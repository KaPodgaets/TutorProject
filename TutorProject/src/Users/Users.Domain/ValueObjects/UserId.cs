using CSharpFunctionalExtensions;

namespace Users.Domain.ValueObjects;

public class UserId : ComparableValueObject
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static UserId NewUserId() => new(Guid.NewGuid());

    public static UserId Empty() => new(Guid.Empty);

    public static UserId Create(Guid id) => new(id);

    public static implicit operator UserId(Guid id) => new(id);

    public static implicit operator Guid(UserId userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        return userId.Value;
    }

    public new string ToString() => Value.ToString();

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
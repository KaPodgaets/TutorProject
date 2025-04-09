using CSharpFunctionalExtensions;

namespace Students.Domain.Schools;

public class SchoolId : ComparableValueObject
{
    private SchoolId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static SchoolId NewSchoolId() => new(Guid.NewGuid());

    public static SchoolId Empty() => new(Guid.Empty);

    public static SchoolId Create(Guid id) => new(id);

    public static implicit operator SchoolId(Guid id) => new(id);

    public static implicit operator Guid(SchoolId userId)
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
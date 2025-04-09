using CSharpFunctionalExtensions;

namespace Students.Domain.Students.Ids;

public class ParentId : ComparableValueObject
{
    private ParentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static ParentId NewStudentId() => new (Guid.NewGuid());

    public static ParentId Empty() => new(Guid.Empty);

    public static ParentId Create(Guid id) => new(id);

    public static implicit operator ParentId(Guid id) => new(id);

    public static implicit operator Guid(ParentId parentId)
    {
        ArgumentNullException.ThrowIfNull(parentId);
        return parentId.Value;
    }

    public new string ToString() => Value.ToString();

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
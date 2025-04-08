using CSharpFunctionalExtensions;

namespace Students.Domain.Students;

public class StudentId : ComparableValueObject
{
    private StudentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; init; }

    public static StudentId NewStudentId() => new(Guid.NewGuid());

    public static StudentId Empty() => new(Guid.Empty);

    public static StudentId Create(Guid id) => new(id);

    public static implicit operator StudentId(Guid id) => new(id);

    public static implicit operator Guid(StudentId userId)
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
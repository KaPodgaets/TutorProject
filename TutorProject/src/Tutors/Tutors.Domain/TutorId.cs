using CSharpFunctionalExtensions;

namespace Tutors.Domain;

public class TutorId : ComparableValueObject
{
    private TutorId(Guid value)
    {
        Value = value;
    }

    public static TutorId None { get; } = new TutorId(Guid.Empty);

    public Guid Value { get; init; }

    public static TutorId NewTutorId() => new(Guid.NewGuid());

    public static TutorId Empty() => new(Guid.Empty);

    public static TutorId Create(Guid id) => new(id);

    public static implicit operator TutorId(Guid id) => new(id);

    public static implicit operator Guid(TutorId userId)
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
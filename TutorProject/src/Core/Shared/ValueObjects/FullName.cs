using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Shared.ValueObjects;

public class FullName : ComparableValueObject
{
    private FullName(
        string firstName,
        string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName None { get; } = new FullName(string.Empty, string.Empty);

    public string FirstName { get; private init; }

    public string LastName { get; private init; }

    public static Result<FullName, ErrorList> Create(
        string firstName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsInvalid(nameof(FirstName)).ToErrorList();
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsInvalid(nameof(LastName)).ToErrorList();

        // TODO add check for name length
        return new FullName(firstName, lastName);
    }

    public override int GetHashCode() =>
        HashCode.Combine(FirstName, LastName);

    public override string ToString() =>
        $"{FirstName} {LastName}";

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
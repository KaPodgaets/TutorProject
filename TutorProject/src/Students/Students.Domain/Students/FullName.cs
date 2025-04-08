using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Students.Domain.Students;

public class FullName : ComparableValueObject
{
    private FullName(
        string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

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

        return new FullName(firstName, lastName);
    }

    public override int GetHashCode() =>
        HashCode.Combine(FirstName, LastName);

    public override string ToString() =>
        $"{FirstName} {LastName}";


    protected override IEnumerable<IComparable> GetComparableEqualityComponents() 
    {
        yield return Value;
    }
}
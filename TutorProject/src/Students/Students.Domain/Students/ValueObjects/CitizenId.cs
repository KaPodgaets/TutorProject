using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Students.Domain.Students.ValueObjects;

public class CitizenId : ComparableValueObject
{
    private CitizenId(string citizenId)
    {
        Value = citizenId;
    }

    public string Value { get; init; }

    public static Result<CitizenId, ErrorList> Create(string citizenId)
    {
        if (string.IsNullOrWhiteSpace(citizenId))
            return Errors.General.ValueIsInvalid(nameof(citizenId)).ToErrorList();

        return new CitizenId(citizenId);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
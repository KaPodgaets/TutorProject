using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Students.Domain.Students.ValueObjects;

public class Passport : ComparableValueObject
{
    private string x = string.Empty;

    private Passport(string number, string country)
    {
        Number = number;
        Country = country;
    }

    public static Passport None { get; } = new Passport(string.Empty, string.Empty);

    public string Number { get; init; }

    public string Country { get; init; }

    public static Result<Passport, ErrorList> Create(string? number, string? country)
    {
        if (string.IsNullOrWhiteSpace(number))
            return Errors.General.ValueIsInvalid(nameof(number)).ToErrorList();

        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsInvalid(nameof(country)).ToErrorList();

        return new Passport(number, country);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Number;
        yield return Country;
    }
}
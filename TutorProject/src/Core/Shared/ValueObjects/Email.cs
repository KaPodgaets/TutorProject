using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Shared.ValueObjects;

/// <summary>
/// Regex is generated as partial.
/// </summary>
public sealed partial class Email : ComparableValueObject
{
    private const string PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<Email, Error> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValueIsInvalid("Email should not be empty");

        return
            MyRegex().IsMatch(email) == false
                ? Errors.General.ValueIsInvalid("Email format is invalid")
                : new Email(email);
    }

    public override string ToString() => Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(PATTERN)]
    private static partial Regex MyRegex();
}
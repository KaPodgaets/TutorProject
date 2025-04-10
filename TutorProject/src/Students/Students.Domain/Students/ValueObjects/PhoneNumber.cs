using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Students.Domain.Students.ValueObjects;

/// <summary>
/// Use this class only for mobile phone numbers.
/// It's partial because of Regex.
/// </summary>
public partial class PhoneNumber : ComparableValueObject
{
    private const string PATTERN = @"^05\d{8}$";

    private PhoneNumber(string phoneNumber)
    {
        Value = phoneNumber;
    }

    public string Value { get; init; }

    public static Result<PhoneNumber, ErrorList> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Errors.General.ValueIsInvalid(nameof(phoneNumber)).ToErrorList();

        if (!MyRegex().IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid(nameof(phoneNumber)).ToErrorList();

        return new PhoneNumber(phoneNumber);
    }

    public string Format() =>
        Value.Length == 10
            ? $"{Value[..3]}-{Value[3..]}"
            : Value;

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(PATTERN)]
    private static partial Regex MyRegex();
}
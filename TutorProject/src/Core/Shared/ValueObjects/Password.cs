using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Shared.ValueObjects;

public sealed class Password : ComparableValueObject
{
    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<Password, Error> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Errors.General.ValueIsInvalid("Password should not be empty");

        if (password.Length < 6)
            return Errors.General.ValueIsInvalid("Password must be at least 6 characters long");

        if (!Regex.IsMatch(password, @"\d"))
            return Errors.General.ValueIsInvalid("Password must contain at least one digit");

        if (!Regex.IsMatch(password, @"[A-Za-z]"))
            return Errors.General.ValueIsInvalid("Password must contain at least one letter");

        return new Password(password);
    }

    public override string ToString() => "[Protected]";

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
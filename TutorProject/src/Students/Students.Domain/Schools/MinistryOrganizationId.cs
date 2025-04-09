using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Students.Domain.Schools;

/// <summary>
/// This class is partial because of regex.
/// </summary>
public partial class MinistryOrganizationId : ComparableValueObject
{
    private const string PATTERN = @"^\d{6}$";

    private MinistryOrganizationId(string citizenId)
    {
        Value = citizenId;
    }

    public string Value { get; init; }

    public static Result<MinistryOrganizationId, ErrorList> Create(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return Errors.General.ValueIsInvalid(nameof(id)).ToErrorList();

        if (!MyRegex().IsMatch(id))
            return Errors.General.ValueIsInvalid(nameof(id)).ToErrorList();

        return new MinistryOrganizationId(id);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(PATTERN)]
    private static partial Regex MyRegex();
}
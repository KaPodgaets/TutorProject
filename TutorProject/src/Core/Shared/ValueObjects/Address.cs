using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Shared.ValueObjects;

public sealed record Address
{
    private Address(
        string streetCode,
        string streetName,
        string cityCode,
        string cityName,
        string buildingNumber,
        string buildingLetter)
    {
        StreetCode = streetCode;
        StreetName = streetName;
        CityCode = cityCode;
        CityName = cityName;
        BuildingNumber = buildingNumber;
        BuildingLetter = buildingLetter;
    }

    public static Address None { get; } =
        new Address(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);

    public string StreetCode { get; set; }

    public string StreetName { get; set; }

    public string CityCode { get; set; }

    public string CityName { get; set; }

    public string BuildingNumber { get; set; }

    public string BuildingLetter { get; set; }

    public static Result<Address, Error> Create(
        string streetCode,
        string streetName,
        string cityCode,
        string cityName,
        string buildingNumber,
        string buildingLetter)
    {
        if (string.IsNullOrWhiteSpace(streetCode))
            return Errors.General.ValueIsRequired(nameof(streetCode));

        if (string.IsNullOrWhiteSpace(cityCode))
            return Errors.General.ValueIsRequired(nameof(cityCode));

        if (string.IsNullOrWhiteSpace(buildingNumber))
            return Errors.General.ValueIsRequired(nameof(buildingNumber));

        return new Address(streetCode, streetName, cityCode, cityName, buildingNumber, buildingLetter);
    }
}
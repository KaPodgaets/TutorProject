namespace Tutors.Contracts.Dtos;

public record AddressDto(
    string StreetCode,
    string StreetName,
    string CityCode,
    string CityName,
    string BuildingNumber,
    string BuildingLetter);
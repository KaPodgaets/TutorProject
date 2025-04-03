namespace TutorProject.Domain.Shared;

public class Address
{
    public Address(
        int streetCode,
        string streetName,
        int cityCode,
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

    public int StreetCode { get; set; }

    public string StreetName { get; set; }

    public int CityCode { get; set; }

    public string CityName { get; set; }

    public string BuildingNumber { get; set; }

    public string BuildingLetter { get; set; }
}
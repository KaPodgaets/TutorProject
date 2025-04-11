namespace Tutors.Contracts.Requests;

public record UpdateTutorRequest(
    string FirstName,
    string LastName,
    string? CitizenId,
    string? PassportNumber,
    string? PassportCountry,
    Guid? SchoolId);
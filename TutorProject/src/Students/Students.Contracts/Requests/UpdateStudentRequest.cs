namespace Students.Contracts.Requests;

public record UpdateStudentRequest(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId);
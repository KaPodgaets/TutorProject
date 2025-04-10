namespace Students.Contracts.Requests;

public record CreateStudentRequest(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId);
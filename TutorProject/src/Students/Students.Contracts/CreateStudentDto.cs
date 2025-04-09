namespace Students.Contracts;

public record CreateStudentDto(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId);

public record UpdateStudentDto(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId);

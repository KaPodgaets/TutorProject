using Shared.Abstractions;

namespace Students.Application.Commands.CreateStudent;

public record UpdateStudentCommand(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId) : ICommand;
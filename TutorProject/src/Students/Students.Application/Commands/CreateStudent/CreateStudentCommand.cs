using Shared.Abstractions;

namespace Students.Application.Commands.CreateStudent;

public record CreateStudentCommand(
    string FirstName,
    string LastName,
    string CitizenId,
    string PassportNumber,
    string PassportCountry,
    Guid? SchoolId) : ICommand;
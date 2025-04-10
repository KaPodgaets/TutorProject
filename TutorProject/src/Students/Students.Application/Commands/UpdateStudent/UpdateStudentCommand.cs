using Shared.Abstractions;

namespace Students.Application.Commands.UpdateStudent;

public record UpdateStudentCommand(
    Guid StudentId,
    string FirstName,
    string LastName,
    string? CitizenId,
    string? PassportNumber,
    string? PassportCountry,
    Guid? SchoolId) : ICommand;
using Shared.Abstractions;

namespace Tutors.Application.Commands.UpdateTutor;

public record UpdateStudentCommand(
    Guid StudentId,
    string FirstName,
    string LastName,
    string? CitizenId,
    string? PassportNumber,
    string? PassportCountry,
    Guid? SchoolId) : ICommand;
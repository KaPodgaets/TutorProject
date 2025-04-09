using Shared.Abstractions;

namespace Students.Application.Commands.CreateStudent;

public record DeleteStudentCommand(Guid StudentId) : ICommand;
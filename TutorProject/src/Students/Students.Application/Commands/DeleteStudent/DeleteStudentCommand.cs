using Shared.Abstractions;

namespace Students.Application.Commands.DeleteStudent;

public record DeleteStudentCommand(Guid StudentId) : ICommand;
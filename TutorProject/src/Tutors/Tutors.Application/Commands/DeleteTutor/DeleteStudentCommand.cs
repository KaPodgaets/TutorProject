using Shared.Abstractions;

namespace Tutors.Application.Commands.DeleteTutor;

public record DeleteStudentCommand(Guid StudentId) : ICommand;
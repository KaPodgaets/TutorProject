using Shared.Abstractions;

namespace Tutors.Application.Commands.DeleteTutor;

public record DeleteTutorCommand(Guid TutorId) : ICommand;
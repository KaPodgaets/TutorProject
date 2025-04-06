using TutorProject.Domain.Shared.Abstractions;

namespace TutorProject.Application.Users.Commands;

public record CreateUserCommand(
    string Email,
    string Password) : ICommand;
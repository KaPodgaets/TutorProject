using TutorProject.Domain.Shared.Abstractions;

namespace TutorProject.Application.Users;

public record CreateUserCommand(
    string Email,
    string Password) : ICommand;
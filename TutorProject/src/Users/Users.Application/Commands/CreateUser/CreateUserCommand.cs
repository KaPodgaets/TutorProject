using Shared.Abstractions;

namespace TutorProject.Application.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password) : ICommand;
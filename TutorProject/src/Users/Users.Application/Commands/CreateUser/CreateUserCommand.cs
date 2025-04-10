using Shared.Abstractions;

namespace Users.Application.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password) : ICommand;
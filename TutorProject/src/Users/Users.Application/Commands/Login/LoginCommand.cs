using Shared.Abstractions;

namespace Users.Application.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand;
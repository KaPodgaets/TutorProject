using Shared.Abstractions;

namespace TutorProject.Application.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand;
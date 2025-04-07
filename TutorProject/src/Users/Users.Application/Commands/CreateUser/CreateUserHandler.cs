using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Errors;
using TutorProject.Application.Database;
using Users.Domain;

namespace TutorProject.Application.Commands.CreateUser;

public class CreateUserHandler : ICommandHandler<Guid, CreateUserCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly CreateUserValidator _validator;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(
        IUsersRepository usersRepository,
        CreateUserValidator validator,
        ILogger<CreateUserHandler> logger)
    {
        _usersRepository = usersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation inputs
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        // business logic validation

        // create new domain entity
        var newUser = User.CreateUser(command.Email, command.Password).Value;

        // use repository + transaction
        var result = await _usersRepository.Create(newUser, cancellationToken);

        if (result.IsFailure)
            return result;

        _logger.LogInformation("User {UserId} created", result.Value);

        return result.Value;
    }
}
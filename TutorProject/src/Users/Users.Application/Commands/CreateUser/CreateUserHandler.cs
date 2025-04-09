using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.ResultPattern;
using Shared.Validation;
using Shared.ValueObjects;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using Users.Domain.Roles;

namespace TutorProject.Application.Commands.CreateUser;

public class CreateUserHandler : ICommandHandler<Guid, CreateUserCommand>
{
    private readonly IRolesRepository _rolesRepository;
    private readonly CreateUserValidator _validator;
    private readonly IUserManager _userManager;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(
        IRolesRepository rolesRepository,
        CreateUserValidator validator,
        ILogger<CreateUserHandler> logger,
        IUserManager userManager)
    {
        _rolesRepository = rolesRepository;
        _validator = validator;
        _logger = logger;
        _userManager = userManager;
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
        var email = Email.Create(command.Email).Value;
        var password = Password.Create(command.Password).Value;

        var existingRole = await _rolesRepository.GetRoleByName(Role.VIEWER, cancellationToken);
        if (existingRole is null)
            return Errors.General.Failure().ToErrorList();

        var registerNewUserResult =
            await _userManager.RegisterNewUserAsync(email, password, [existingRole], cancellationToken);
        if (registerNewUserResult.IsFailure)
            return registerNewUserResult.Error;

        _logger.LogInformation("User {UserId} created", registerNewUserResult.Value.Id.Value);

        return registerNewUserResult.Value.Id.Value;
    }
}
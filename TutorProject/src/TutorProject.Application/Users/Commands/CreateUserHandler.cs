using CSharpFunctionalExtensions;
using TutorProject.Domain.Shared.Abstractions;
using TutorProject.Domain.Shared.Errors;
using TutorProject.Domain.Users;

namespace TutorProject.Application.Users.Commands;

public class CreateUserHandler : ICommandHandler<Guid, CreateUserCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly CreateUserValidator _validator;

    public CreateUserHandler(
        IUsersRepository usersRepository,
        CreateUserValidator validator)
    {
        _usersRepository = usersRepository;
        _validator = validator;
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
        return result.IsFailure
            ? result
            : result.Value;
    }
}
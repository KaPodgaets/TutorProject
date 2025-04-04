using CSharpFunctionalExtensions;
using TutorProject.Domain.Shared;
using TutorProject.Domain.Shared.Abstractions;

namespace TutorProject.Application.Users;

public class CreateUserHandler : ICommandHandler<Guid, CreateUserCommand>
{
    public async Task<Result<Guid, ErrorList>> ExecuteAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        // return default(Result<Guid, ErrorList>);
        return Guid.NewGuid();
    }
}
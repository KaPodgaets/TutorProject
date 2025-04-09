using CSharpFunctionalExtensions;
using Shared.Abstractions;
using Shared.ResultPattern;

namespace Students.Application.Commands.CreateStudent;

public class DeleteUserHandler : ICommandHandler<Guid, DeleteStudentCommand>
{
    public Task<Result<Guid, ErrorList>> ExecuteAsync(
        DeleteStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
using CSharpFunctionalExtensions;
using Shared.Abstractions;
using Shared.ResultPattern;

namespace Students.Application.Commands.DeleteStudent;

public class DeleteStudentHandler : ICommandHandler<Guid, DeleteStudentCommand>
{
    public Task<Result<Guid, ErrorList>> ExecuteAsync(
        DeleteStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
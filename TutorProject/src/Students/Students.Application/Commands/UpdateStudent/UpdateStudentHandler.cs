using CSharpFunctionalExtensions;
using Shared.Abstractions;
using Shared.ResultPattern;

namespace Students.Application.Commands.CreateStudent;

public class UpdateStudentHandler : ICommandHandler<Guid, UpdateStudentCommand>
{
    public Task<Result<Guid, ErrorList>> ExecuteAsync(
        UpdateStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
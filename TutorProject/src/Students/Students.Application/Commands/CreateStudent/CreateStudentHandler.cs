using CSharpFunctionalExtensions;
using Shared.Abstractions;
using Shared.ResultPattern;

namespace Students.Application.Commands.CreateStudent;

public class CreateStudentHandler : ICommandHandler<Guid, CreateStudentCommand>
{
    public Task<Result<Guid, ErrorList>> ExecuteAsync(
        CreateStudentCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
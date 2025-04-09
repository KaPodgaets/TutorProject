using CSharpFunctionalExtensions;
using Shared.ResultPattern;

namespace Shared.Abstractions;

public interface ICommandHandler<TValue, in TCommand>
    where TCommand : ICommand
{
    Task<Result<TValue, ErrorList>> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<Result<ErrorList>> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
}
using CSharpFunctionalExtensions;

namespace TutorProject.Domain.Shared.Abstractions;

public interface IQueryHandler<TValue, in TQuery>
    where TQuery : IQuery
{
    Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
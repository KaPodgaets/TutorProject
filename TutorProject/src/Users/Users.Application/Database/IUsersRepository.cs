using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Users.Domain;

namespace Users.Application.Database;

public interface IUsersRepository
{
    Task<Result<User, ErrorList>> Create(User model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Delete(User model, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> Update(User model, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> GetById(Guid id, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> GetByEmail(
        Email email,
        CancellationToken cancellationToken = default);
}
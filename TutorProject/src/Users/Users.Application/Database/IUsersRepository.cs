using CSharpFunctionalExtensions;
using Shared.Errors;
using Users.Domain.Users;

namespace TutorProject.Application.Users;

public interface IUsersRepository
{
    Task<Result<Guid, ErrorList>> Create(User model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Delete(User model, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> Update(User model, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> GetById(Guid id, CancellationToken cancellationToken);

    Task<Result<User, ErrorList>> GetByEmail(
        string email,
        CancellationToken cancellationToken = default);
}
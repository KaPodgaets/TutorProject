using CSharpFunctionalExtensions;
using TutorProject.Domain.Shared;
using TutorProject.Domain.Shared.Errors;
using TutorProject.Domain.Users;

namespace TutorProject.Application;

public interface IUsersRepository
{
    Task<Result<Guid, ErrorList>> Create(User newUser);

    Task<Result<Guid, ErrorList>> Delete(User user);

    Task<Result<User, ErrorList>> Update(User user);

    Task<Result<User, ErrorList>> GetById(Guid id);
}
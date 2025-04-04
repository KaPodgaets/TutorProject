using CSharpFunctionalExtensions;
using TutorProject.Domain.Users;

namespace TutorProject.Application;

public interface IUsersRepository
{
    Task<Result> Create(User newUser);

    Task<Result> Delete(User user);

    Task<Result> Update(User user);

    Task<Result> GetById(Guid id);
}
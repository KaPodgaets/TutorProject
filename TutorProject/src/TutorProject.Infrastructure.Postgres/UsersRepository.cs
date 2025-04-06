using CSharpFunctionalExtensions;
using TutorProject.Application.Users;
using TutorProject.Domain.Shared.Errors;
using TutorProject.Domain.Users;

namespace TutorProject.Infrastructure.Postgres;

public class UsersRepository : IUsersRepository
{
    public async Task<Result<Guid, ErrorList>> Create(User newUser)
    {
        await Task.CompletedTask;

        return newUser.Id;
    }

    public Task<Result<Guid, ErrorList>> Delete(User user) => throw new NotImplementedException();

    public Task<Result<User, ErrorList>> Update(User user) => throw new NotImplementedException();

    public Task<Result<User, ErrorList>> GetById(Guid id) => throw new NotImplementedException();
}
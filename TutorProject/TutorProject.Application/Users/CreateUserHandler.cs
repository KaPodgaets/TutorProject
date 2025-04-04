using CSharpFunctionalExtensions;
using TutorProject.Domain.Shared;

namespace TutorProject.Application.Users;

public class CreateUserHandler
{
    public async Task<Result> ExecuteAsync(CreateUserCommand command)
    {
        await Task.CompletedTask;
        return Result.Success();
    }
}
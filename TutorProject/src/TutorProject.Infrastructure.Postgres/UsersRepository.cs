using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TutorProject.Application.Users;
using TutorProject.Domain.Shared.Errors;
using TutorProject.Domain.Users;

namespace TutorProject.Infrastructure.Postgres;

public class UsersRepository : IUsersRepository
{
    private readonly UsersDbContext _dbContext;

    public UsersRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> Create(User model, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.Users.AddAsync(model, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Errors.General.Failure().ToErrorList();
        }

        return model.Id;
    }

    public async Task<Result<Guid, ErrorList>> Delete(User model, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id;
    }

    public async Task<Result<User, ErrorList>> Update(User model, CancellationToken cancellationToken)
    {
        _dbContext.Users.Attach(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model;
    }

    public async Task<Result<User, ErrorList>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(id).ToErrorList();
        }

        return model;
    }

    public async Task<Result<User, ErrorList>> GetByEmail(
        string email,
        CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(email).ToErrorList();
        }

        return model;
    }
}
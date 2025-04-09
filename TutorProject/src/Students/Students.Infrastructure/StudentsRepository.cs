using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shared.ResultPattern;
using Students.Application.Database;
using Students.Domain.Students;
using Students.Domain.Students.ValueObjects;
using Students.Infrastructure.DbContext;

namespace Students.Infrastructure;

public class StudentsRepository : IStudentsRepository
{
    private readonly StudentsDbContext _dbContext;

    public StudentsRepository(StudentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> Create(Student model, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.Students.AddAsync(model, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Errors.General.Failure().ToErrorList();
        }

        return model.Id.Value;
    }

    public async Task<Result<Guid, ErrorList>> Delete(Student model, CancellationToken cancellationToken)
    {
        _dbContext.Students.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<Guid, ErrorList>> Update(Student model, CancellationToken cancellationToken)
    {
        _dbContext.Students.Attach(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<Student, ErrorList>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _dbContext.Students
            .Include(x => x.Parents)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(id).ToErrorList();
        }

        return model;
    }

    public async Task<Result<Student, ErrorList>> GetByCitizenId(
        CitizenId citizenId,
        CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Students
            .Include(x => x.Parents)
            .FirstOrDefaultAsync(x => x.CitizenId == citizenId, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(citizenId.Value).ToErrorList();
        }

        return model;
    }

    public async Task<Result<Student, ErrorList>> GetByPassport(
        Passport passport,
        CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Students
            .Include(x => x.Parents)
            .FirstOrDefaultAsync(x => x.Passport == passport, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(passport.Number).ToErrorList();
        }

        return model;
    }
}
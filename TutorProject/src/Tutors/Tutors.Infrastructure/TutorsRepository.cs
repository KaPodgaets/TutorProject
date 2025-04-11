using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Tutors.Application.Database;
using Tutors.Domain;
using Tutors.Infrastructure.DbContext;

namespace Tutors.Infrastructure;

public class TutorsRepository : ITutorsRepository
{
    private readonly TutorsDbContext _dbContext;

    public TutorsRepository(TutorsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> Create(Tutor model, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.Tutors.AddAsync(model, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Errors.General.Failure().ToErrorList();
        }

        return model.Id.Value;
    }

    public async Task<Result<Guid, ErrorList>> Delete(Tutor model, CancellationToken cancellationToken)
    {
        _dbContext.Tutors.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<Guid, ErrorList>> Update(Tutor model, CancellationToken cancellationToken)
    {
        _dbContext.Tutors.Attach(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<Tutor, ErrorList>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await _dbContext.Tutors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(id).ToErrorList();
        }

        return model;
    }

    public async Task<Result<Tutor, ErrorList>> GetByCitizenId(
        CitizenId citizenId,
        CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Tutors
            .FirstOrDefaultAsync(x => x.CitizenId == citizenId, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(citizenId.Value).ToErrorList();
        }

        return model;
    }
}
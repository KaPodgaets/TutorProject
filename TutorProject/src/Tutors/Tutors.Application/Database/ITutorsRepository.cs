using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Tutors.Domain;

namespace Tutors.Application.Database;

public interface ITutorsRepository
{
    Task<Result<Guid, ErrorList>> Create(Tutor model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Delete(Tutor model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Update(Tutor model, CancellationToken cancellationToken);

    Task<Result<Tutor, ErrorList>> GetById(Guid id, CancellationToken cancellationToken);

    Task<Result<Tutor, ErrorList>> GetByCitizenId(
        CitizenId citizenId,
        CancellationToken cancellationToken = default);
}
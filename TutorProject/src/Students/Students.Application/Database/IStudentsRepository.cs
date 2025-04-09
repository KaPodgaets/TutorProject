using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Students.Domain.Students;
using Students.Domain.Students.ValueObjects;

namespace Students.Application.Database;

public interface IStudentsRepository
{
    Task<Result<Guid, ErrorList>> Create(Student model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Delete(Student model, CancellationToken cancellationToken);

    Task<Result<Guid, ErrorList>> Update(Student model, CancellationToken cancellationToken);

    Task<Result<Student, ErrorList>> GetById(Guid id, CancellationToken cancellationToken);

    Task<Result<Student, ErrorList>> GetByCitizenId(
        CitizenId citizenId,
        CancellationToken cancellationToken = default);

    Task<Result<Student, ErrorList>> GetByPassport(
        Passport passport,
        CancellationToken cancellationToken = default);
}
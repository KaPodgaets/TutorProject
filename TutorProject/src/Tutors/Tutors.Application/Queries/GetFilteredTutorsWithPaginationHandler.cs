using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Extensions;
using Shared.Models;
using Tutors.Application.Database;
using Tutors.Domain;

namespace Tutors.Application.Queries;

public class GetFilteredTutorsWithPaginationHandler
    : IQueryHandler<PagedList<Tutor>, GetFilteredTutorsWithPaginationQuery>
{
    private readonly ITutorsReadDbContext _readDbContext;

    public GetFilteredTutorsWithPaginationHandler(
        ITutorsReadDbContext readDbContext,
        ILogger<GetFilteredTutorsWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<Tutor>>> HandleAsync(
        GetFilteredTutorsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var studentsQuery = _readDbContext.Tutors;

        studentsQuery = studentsQuery
            .WhereIf(
                query.TutorId is not null,
                p => p.Id == query.TutorId)
            .WhereIf(
                query.CitizenId is not null,
                p => p.CitizenId.Value == query.CitizenId)
            .WhereIf(
                query.FirstName is not null,
                p => p.FullName.FirstName == query.FirstName)
            .WhereIf(
                query.LastName is not null,
                p => p.FullName.LastName == query.LastName);

        var pagedList = await studentsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}
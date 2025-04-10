using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Shared.Extensions;
using Shared.Models;
using Students.Application.Database;
using Students.Domain.Students;

namespace Students.Application.Queries;

public class GetFilteredStudentsWithPaginationHandler
    : IQueryHandler<PagedList<Student>, GetFilteredStudentsWithPaginationQuery>
{
    private readonly IStudentsDbContext _queryContext;

    public GetFilteredStudentsWithPaginationHandler(
        IStudentsDbContext queryContext,
        ILogger<GetFilteredStudentsWithPaginationHandler> logger)
    {
        _queryContext = queryContext;
    }

    public async Task<Result<PagedList<Student>>> HandleAsync(
        GetFilteredStudentsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var studentsQuery = _queryContext.Students.AsQueryable();

        studentsQuery = studentsQuery
            .WhereIf(
                query.StudentId is not null,
                p => p.Id == query.StudentId)
            .WhereIf(
                query.ParentId is not null,
                p => p.Parents.Any(x => x.Id.Value == query.ParentId))
            .WhereIf(
                query.SchoolId is not null,
                p => p.SchoolId == query.SchoolId)
            .WhereIf(
                query.IsNeedTutor is not null,
                p => p.TutorHoursNeeded > 0 == query.IsNeedTutor)
            .WhereIf(
                query.HasTutor is not null,
                p => (p.TutorId != null) == query.HasTutor);

        var pagedList = await studentsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}
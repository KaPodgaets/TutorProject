using Shared.Abstractions;

namespace Students.Application.Queries;

public record GetFilteredStudentsWithPaginationQuery(
    Guid? StudentId,
    Guid? SchoolId,
    Guid? ParentId,
    bool? IsNeedTutor,
    bool? HasTutor,
    int Page,
    int PageSize) : IQuery;
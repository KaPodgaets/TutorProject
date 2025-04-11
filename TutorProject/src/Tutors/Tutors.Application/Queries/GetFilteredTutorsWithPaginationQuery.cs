using Shared.Abstractions;

namespace Tutors.Application.Queries;

public record GetFilteredTutorsWithPaginationQuery(
    Guid? TutorId,
    string? FirstName,
    string? LastName,
    string? CitizenId,
    int Page,
    int PageSize) : IQuery;
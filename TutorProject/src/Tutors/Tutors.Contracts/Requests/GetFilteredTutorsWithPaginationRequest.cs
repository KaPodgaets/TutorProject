namespace Tutors.Contracts.Requests;

public record GetFilteredTutorsWithPaginationRequest(
    Guid? TutorId,
    string? FirstName,
    string? LastName,
    string? CitizenId,
    int Page,
    int PageSize);
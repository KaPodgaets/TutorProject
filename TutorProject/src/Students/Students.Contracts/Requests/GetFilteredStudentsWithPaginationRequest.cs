namespace Students.Contracts.Requests;

public record GetFilteredStudentsWithPaginationRequest(
    Guid? StudentId,
    Guid? SchoolId,
    Guid? ParentId,
    bool? IsNeedTutor,
    bool? HasTutor,
    int Page,
    int PageSize);
using Tutors.Contracts.Requests;

namespace Tutors.Presentation;

public static class ContractRequestExtensions
{
    public static CreateStudentCommand ToCommand(this CreateTutorRequest request)
    {
        return new CreateStudentCommand(
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.PassportNumber,
            request.PassportCountry,
            request.SchoolId);
    }

    public static UpdateStudentCommand ToCommand(this UpdateTutorRequest request, Guid studentId)
    {
        return new UpdateStudentCommand(
            studentId,
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.PassportNumber,
            request.PassportCountry,
            request.SchoolId);
    }

    public static GetFilteredStudentsWithPaginationQuery ToQuery(this GetFilteredTutorsWithPaginationRequest request)
    {
        return new GetFilteredStudentsWithPaginationQuery(
            request.StudentId,
            request.SchoolId,
            request.ParentId,
            request.IsNeedTutor,
            request.HasTutor,
            request.Page,
            request.PageSize);
    }
}
using Students.Application.Commands.CreateStudent;
using Students.Application.Commands.UpdateStudent;
using Students.Application.Queries;
using Students.Contracts;
using Students.Contracts.Requests;

namespace Students.Presentation;

public static class ContractRequestExtensions
{
    public static CreateStudentCommand ToCommand(this CreateStudentRequest request)
    {
        return new CreateStudentCommand(
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.PassportNumber,
            request.PassportCountry,
            request.SchoolId);
    }

    public static UpdateStudentCommand ToCommand(this UpdateStudentRequest request, Guid studentId)
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

    public static GetFilteredStudentsWithPaginationQuery ToQuery(this GetFilteredStudentsWithPaginationRequest request)
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
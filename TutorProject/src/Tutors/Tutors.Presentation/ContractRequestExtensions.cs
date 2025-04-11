using Tutors.Application.Commands.CreateTutor;
using Tutors.Application.Queries;
using Tutors.Contracts.Requests;

namespace Tutors.Presentation;

public static class ContractRequestExtensions
{
    public static CreateTutorCommand ToCommand(this CreateTutorRequest request)
    {
        return new CreateTutorCommand(
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.Address,
            request.PhoneNumber,
            request.Email);
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

    public static GetFilteredTutorsWithPaginationQuery ToQuery(this GetFilteredTutorsWithPaginationRequest request)
    {
        return new GetFilteredTutorsWithPaginationQuery(
            request.TutorId,
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.Page,
            request.PageSize);
    }
}
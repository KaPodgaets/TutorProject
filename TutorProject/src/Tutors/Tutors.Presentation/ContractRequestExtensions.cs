using Tutors.Application.Commands.CreateTutor;
using Tutors.Application.Commands.UpdateTutor;
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

    public static UpdateTutorCommand ToCommand(this UpdateTutorRequest request, Guid studentId)
    {
        return new UpdateTutorCommand(
            request.FirstName,
            request.LastName,
            request.CitizenId,
            request.Address,
            request.PhoneNumber,
            request.Email);
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
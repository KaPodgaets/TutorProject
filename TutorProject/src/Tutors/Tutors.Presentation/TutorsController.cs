using Framework;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Tutors.Application.Commands.CreateTutor;
using Tutors.Application.Commands.DeleteTutor;
using Tutors.Application.Commands.UpdateTutor;
using Tutors.Application.Queries;
using Tutors.Contracts.Requests;

namespace Tutors.Presentation;

// TODO add soft delete endpoint and force delete endpoint
[ApiController]
[Route("[controller]")]
public class TutorsController : ApplicationController
{
    // [Permission(Permissions.Students.CREATE)]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTutorRequest request,
        [FromServices] CreateTutorHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFilteredWithPagination(
        [FromQuery] GetFilteredTutorsWithPaginationRequest request,
        [FromServices] GetFilteredTutorsWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await handler.HandleAsync(query, cancellationToken);
        return Ok(result.Value);
    }

    [HttpPatch("{studentId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid studentId,
        [FromBody] UpdateTutorRequest request,
        [FromServices] UpdateTutorHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(studentId);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }

    [HttpDelete("{tutorId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid tutorId,
        [FromServices] DeleteTutorHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTutorCommand(tutorId);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }
}
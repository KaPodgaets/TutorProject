using Microsoft.AspNetCore.Mvc;
using Shared;
using Tutors.Contracts.Requests;

namespace Tutors.Presentation;

[ApiController]
[Route("[controller]")]
public class StudentsController : ApplicationController
{
    // [Permission(Permissions.Students.CREATE)]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTutorRequest request,
        [FromServices] CreateStudentHandler handler,
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
        [FromServices] GetFilteredStudentsWithPaginationHandler handler,
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
        [FromServices] UpdateStudentHandler handler,
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

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid userId,
        [FromServices] DeleteStudentHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteStudentCommand(userId);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }
}
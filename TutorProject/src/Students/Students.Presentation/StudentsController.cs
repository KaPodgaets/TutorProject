using Framework;
using Framework.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Students.Application.Commands.CreateStudent;
using Students.Contracts;

namespace Students.Presentation;

[ApiController]
[Route("[controller]")]
public class StudentsController : ApplicationController
{
    // [Permission(Permissions.Students.CREATE)]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStudentDto dto,
        [FromServices] CreateStudentHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateStudentCommand();
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] UpdateStudentDto dto,
        [FromServices] UpdateStudentHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateStudentCommand();
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
        [FromServices])
    {
        await Task.CompletedTask;

        return Ok("result");
    }
}
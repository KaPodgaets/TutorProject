using Framework;
using Microsoft.AspNetCore.Mvc;
using Shared;
using TutorProject.Application.Commands.CreateUser;
using Users.Contracts.Dtos;

namespace Users.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ApplicationController
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] NewUserDto user,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(user.Email, user.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn(
        [FromBody] NewUserDto user,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(user.Email, user.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        await Task.CompletedTask;

        return Ok(result.Value);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogOut([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }
}
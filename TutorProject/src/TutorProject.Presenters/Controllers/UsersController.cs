using Microsoft.AspNetCore.Mvc;

namespace TutorProject.Presenters.Controllers;

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
    public async Task<IActionResult> Create()
    {
        await Task.CompletedTask;

        return Ok("result");
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
    public async Task<IActionResult> LogIn(string email, string password)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogOut([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }
}